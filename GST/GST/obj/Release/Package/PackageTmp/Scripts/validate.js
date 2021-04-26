//var regEx = /<\s*script[^>]*>[\s\S]*?<\s*\/script>/ig
// validate.js v 1.98
// a generic form validator 
// by Brian Lalonde http://webcoder.info/downloads/
// This work is licensed under the Creative Commons Attribution-Share Alike 3.0 License. 
// To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/3.0/ 
// or send a letter to Creative Commons, 543 Howard Street, 5th Floor, San Francisco, California, 94105, USA.

var status;
var GLOBAL_ie = /MSIE/.test(navigator.userAgent) || /Trident/.test(navigator.userAgent);
var GLOBAL_moz = !GLOBAL_ie && navigator.product == "Gecko";
if (!window.jQuery) {
    document.writeln("<script type=\'text/javascript\' src=\'html5/scripts/jquery-1.8.2.min.js\'></script>");
}

function validZip(fld) {
    var zip = fld.value;
    var zipRegExp = /(^\d{5}$)|(^\D{1}\d{1}\D{1}\s\d{1}\D{1}\d{1}$)/;
    return zipRegExp.test(zip);
}

function validPhone(fld) {
    var phoneNumberPattern = /^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/;
    return phoneNumberPattern.test(fld.value);
}

function requireValue(fld) { // disallow a blank field
    if (fld.disabled) return true;
    if (trim(fld.value) == "" || !fld.value.length)
    { status = 'The field cannot be left blank.'; return false; }
    return true;
}



function allowChars(fld, chars) { // provide a string of acceptable chars for a field
    if (fld.disabled) return true;
    for (var i = 0; i < fld.value.length; i++) {
        if (chars.indexOf(fld.value.charAt(i)) == -1)
        { status = 'The ' + fieldname(fld) + ' field may not contain "' + fld.value.charAt(i) + '" characters.'; return false; }
    }
    return true;
}

function disallowChars(fld, chars) { // provide a string of unacceptable chars for a field
    if (fld.disabled) return true;
    for (var i = 0; i < fld.value.length; i++) {
        if (chars.indexOf(fld.value.charAt(i)) != -1)
        { status = 'The ' + fieldname(fld) + ' field may not contain "' + fld.value.charAt(i) + '" characters.'; return false; }
    }
    return true;
}

function checkEmail(fld) { // simple email check
    if (!fld.value.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var phony = /@(\w+\.)*example\.(com|net|org)$/i;
    if (phony.test(fld.value))
    { status = 'Please enter your email address in the ' + fieldname(fld) + ' field.'; return false; }
    var emailfmt = /^\w+([.-]\w+)*@\w+([.-]\w+)*\.\w{2,8}$/;
    if (!emailfmt.test(fld.value))
    { status = 'The ' + fieldname(fld) + ' field must contain a valid email address.'; return false; }
    return true;
}


function fixInt(fld, sep) { // integer check/complainer 
    if (!fld.value.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var val = fld.value;
    if (typeof (sep) != 'undefined') val = val.replace(new RegExp(sep, 'g'), '');
    val = parseInt(val);
    if (isNaN(val)) { // parse error 
        status = 'The field must contain a number.';
        return false;
    }
    fld.value = val;
    return true;
}

function fixFloat(fld, sep) { // decimal number check/complainer 
    if (!fld.value.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var val = fld.value;
    if (typeof (sep) != 'undefined') val = val.replace(new RegExp(sep, 'g'), '');
    val = parseFloat(fld.value);
    if (isNaN(val)) { // parse error 
        status = 'The ' + fieldname(fld) + ' field must contain a number.';
        return false;
    }
    fld.value = val;
    return true;
}

function fixMoney(fld, sep) { // monetary field check
    if (!fld.value.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var val = fld.value;
    if (typeof (sep) != 'undefined') val = val.replace(new RegExp(sep, 'g'), '');
    if (val.indexOf('$') == 0)
        val = parseFloat(val.substring(1, 40));
    else
        val = parseFloat(val);
    if (isNaN(val)) { // parse error 
        status = 'The ' + fieldname(fld) + ' field must contain a dollar amount.';
        return false;
    }
    var sign = (val < 0 ? '-' : '');
    val = Number(Math.round(Math.abs(val) * 100)).toString();
    while (val.length < 2) val = '0' + val;
    var len = val.length;
    val = sign + (len == 2 ? '0' : val.substring(0, len - 2)) + '.' + val.substring(len - 2, len + 1);
    fld.value = val;
    return true;
}

function fixFixed(fld, dec, sep) { // fixed decimal fields 
    if (!fld.value.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var val = fld.value;
    if (typeof (sep) != 'undefined') val = val.replace(new RegExp(sep, 'g'), '');
    val = parseFloat(fld.value);
    if (isNaN(val)) { // parse error 
        status = 'The ' + fieldname(fld) + ' field must contain a number.';
        return false;
    }
    var sign = (val < 0 ? '-' : '');
    val = Number(Math.round(Math.abs(val) * Math.pow(10, dec))).toString();
    while (val.length < dec) val = '0' + val;
    var len = val.length;
    val = sign + (len == dec ? '0' : val.substring(0, len - dec)) + '.' + val.substring(len - dec, len + 1);
    fld.value = val;
    return true;
}

function fixDate(fld) { // tenacious date correction 
    if (!fld.value.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var val = fld.value;
    var dt = new Date(val.replace(/\D/g, '/'));
    if (!dt.valueOf()) { // the date was unparseable 
        status = 'The ' + fieldname(fld) + ' field has the wrong date.';
        return false;
    }
    fld.value = (dt.getMonth() + 1) + '/' + dt.getDate() + '/' + dt.getFullYear();
    return true;
}

function fixTime(fld, starthour) { // tenacious time correction 
    if (!fld.value.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var hour = 0;
    var mins = 0;
    var ampm = 'am';
    val = fld.value;
    var dt = new Date('1/1/2000 ' + val);
    if (('9' + val) == parseInt('9' + val))
    { hour = val; }
    else if (dt.valueOf())
    { hour = dt.getHours(); mins = dt.getMinutes(); }
    else {
        val = val.replace(/\D+/g, ':');
        hour = parseInt(val);
        mins = parseInt(val.substring(val.indexOf(':') + 1, 20));
        if (val.indexOf('pm') > -1) ampm = 'pm';
        if (isNaN(hour)) hour = 0;
        if (isNaN(mins)) mins = 0;
    }
    if (hour < starthour) { ampm = 'pm'; }
    while (hour > 12) { hour -= 12; ampm = 'pm'; }
    while (mins > 60) { mins -= 60; hour++; }
    if (mins < 10) mins = '0' + mins;
    if (!hour) { // the date was unparseable 
        status = 'The ' + fieldname(fld) + ' field has the wrong time.';
        return false;
    }
    fld.value = hour + ':' + mins + ampm;
    return true;
}

function fixCreditCard(fld) { // tenacious credit card correction; fieldname isn't a big consideration, probably only one card per form 
    if (!fld.value.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var val = fld.value, ctype = 'credit card';
    val = val.replace(/\D/g, '');
    var prefix2 = parseInt(val.substr(0, 2));
    if (val.substr(0, 1) == '4') { // Visa
        ctype = 'Visa\xae';
        if (val.length == 16);
        else if (val.length == 13); // very old #, should be reassigned
        else if (val.length < 13)
        { status = 'The Visa\xae number you provided is not long enough.'; return false; }
        else if (val.length > 16)
        { status = 'The Visa\xae number you provided is too long.'; return false; }
        else
        { status = 'The Visa\xae number you provided is either not long enough, or too long.'; return false; }
    }
    else if (prefix2 >= 51 && prefix2 <= 55) { // MC
        ctype = 'MasterCard\xae';
        if (val.length < 16)
        { status = 'The MasterCard\xae number you provided is not long enough.'; return false; }
        else if (val.length > 16)
        { status = 'The MasterCard\xae number you provided is too long.'; return false; }
    }
    else if ((prefix2 == 34) || (prefix2 == 37)) { // AmEx
        ctype = 'American Express\xae card';
        if (val.length < 15)
        { status = 'The American Express\xae card number you provided is not long enough.'; return false; }
        else if (val.length > 15)
        { status = 'The American Express\xae card number you provided is too long.'; return false; }
    }
    else if (val.substr(0, 4) == '6011') { // Novus/Discover
        ctype = 'Discover\xae card';
        if (val.length < 16)
        { status = 'The Discover\xae card number you provided is not long enough.'; return false; }
        else if (val.length > 16)
        { status = 'The Discover\xae card number you provided is too long.'; return false; }
    }
    else { // other
        if (val.length < 13)
        { status = 'The credit card number you provided is not long enough.'; return false; }
        if (val.length > 19)
        { status = 'The credit card number you provided is too long.'; return false; }
    }
    var sum = 0, dbl = false;
    for (var i = val.length - 1; i >= 0; i--) {
        var digit = parseInt(val.charAt(i)) * ((dbl = !dbl) ? 1 : 2);
        sum += (digit > 9 ? (digit % 10) + 1 : digit);
    }
    if (sum % 10) {
        status = 'The ' + ctype + ' number you provided is not valid.\nPlease double-check it and try again.';
        return false;
    }
    fld.value = val;
    return true;
}

function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}
function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

function querySt(ji, dt) {
    hu = (dt == undefined ? window.location.search.substring(1) : dt);
    gy = hu.split("&");
    for (i = 0; i < gy.length; i++) {
        ft = gy[i].split("=");
        if (ft[0] == ji) {
            return ft[1];
        }
    }
    return "";
}
function trim(stringToTrim) {
    return $.trim(stringToTrim)
    // return stringToTrim.replace(/^\s+|\s+$/g, "");
}
function trimnull(stringToTrim) {
    if (stringToTrim == undefined || stringToTrim == null) {
        return undefined;
    } else {
        return stringToTrim.replace(/\0/g, "");
    }
}

function DoDec1(val) {
    val = unescape(val); val = val.replace(/'/g, '&#39;'); return val;
}
function DoEnc1(val) {
    if (val.indexOf('&#39;') != -1)
        val = val.replace(/&#39;/g, "'"); val = escape(val); val = val.replace(/%20/g, ' ').replace(/%2C/g, ',').replace(/%3A/g, ':').replace(/%28/g, '(').replace(/%29/g, ')');
    return val;
}

function checkDate(elm) {
    if (elm.value.length == 0) return "";
    var strmsg = "Invalid Date";
    var tmp = elm.value.split("-");
    if (tmp.length == 3) strmsg = isDate2(tmp[1] + "/" + tmp[2] + "/" + tmp[0]);
    return strmsg;
}

function doEnc(fld) {
    var val = (fld.length == 0 ? '' : encodeURIComponent(fld));
    val = val.replace(/'/g, escape("'"));
    //val=val.replace(/%C2%BA/g,escape('�'));
    return val;
}
function redoEnc(fld) {
    var val = (fld.length == 0 ? '' : decodeURIComponent(fld));
    val = val.replace(/'/g, escape("'"));
    //val=val.replace(/%C2%BA/g,escape('�'));
    return val;
}

var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (value, length) {
		    value = String(value);
		    length = parseInt(length) || 2;
		    while (value.length < length)
		        value = "0" + value;
		    return value;
		};

    // Regexes and supporting functions are cached through closure
    return function (date, mask) {
        // Treat the first argument as a mask if it doesn't contain any numbers
        if (
			arguments.length == 1 &&
			(typeof date == "string" || date instanceof String) &&
			!/\d/.test(date)
		) {
            mask = date;
            date = undefined;
        }

        date = date ? new Date(date) : new Date();
        if (isNaN(date)) return "";
        var dF = dateFormat;
        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        var d = date.getDate(),
			D = date.getDay(),
			m = date.getMonth(),
			y = date.getFullYear(),
			H = date.getHours(),
			M = date.getMinutes(),
			s = date.getSeconds(),
			L = date.getMilliseconds(),
			o = date.getTimezoneOffset(),
			flags = {
			    d: d,
			    dd: pad(d),
			    ddd: dF.i18n.dayNames[D],
			    dddd: dF.i18n.dayNames[D + 7],
			    m: m + 1,
			    mm: pad(m + 1),
			    mmm: dF.i18n.monthNames[m],
			    mmmm: dF.i18n.monthNames[m + 12],
			    yy: String(y).slice(2),
			    yyyy: y,
			    h: H % 12 || 12,
			    hh: pad(H % 12 || 12),
			    H: H,
			    HH: pad(H),
			    M: M,
			    MM: pad(M),
			    s: s,
			    ss: pad(s),
			    l: pad(L, 3),
			    L: pad(L > 99 ? Math.round(L / 10) : L),
			    t: H < 12 ? "a" : "p",
			    tt: H < 12 ? "am" : "pm",
			    T: H < 12 ? "A" : "P",
			    TT: H < 12 ? "AM" : "PM",
			    Z: (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
			    o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4)
			};

        return mask.replace(token, function ($0) {
            return ($0 in flags) ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
}();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm d yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    fullDateTime: "dddd, mmmm d, yyyy h:MM TT Z",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    onlyTime: "HH:MM",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoFullDateTime: "yyyy-mm-dd'T'HH:MM:ss.lo"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
    ],
    monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
    ]
};

// For convenience...
Date.prototype.format = function (mask) {
    return dateFormat(this, mask);
}


// used by dateAdd, dateDiff, datePart, weekdayName, and monthName
// note: less strict than VBScript's isDate, since JS allows invalid dates to overflow (e.g. Jan 32 transparently becomes Feb 1)
function isDate(p_Expression) {
    return !isNaN(new Date(p_Expression)); 	// <<--- this needs checking
}


// REQUIRES: isDate()
function dateAdd(p_Interval, p_Number, p_Date) {
    if (!isDate(p_Date)) { return "invalid date: '" + p_Date + "'"; }
    if (isNaN(p_Number)) { return "invalid number: '" + p_Number + "'"; }

    p_Number = new Number(p_Number);
    var dt = new Date(p_Date);
    switch (p_Interval.toLowerCase()) {
        case "yyyy":
            {// year
                dt.setFullYear(dt.getFullYear() + p_Number);
                break;
            }
        case "q":
            {		// quarter
                dt.setMonth(dt.getMonth() + (p_Number * 3));
                break;
            }
        case "m":
            {		// month
                dt.setMonth(dt.getMonth() + p_Number);
                break;
            }
        case "y": 	// day of year
        case "d": 	// day
        case "w":
            {		// weekday
                dt.setDate(dt.getDate() + p_Number);
                break;
            }
        case "ww":
            {	// week of year
                dt.setDate(dt.getDate() + (p_Number * 7));
                break;
            }
        case "h":
            {		// hour
                dt.setHours(dt.getHours() + p_Number);
                break;
            }
        case "n":
            {		// minute
                dt.setMinutes(dt.getMinutes() + p_Number);
                break;
            }
        case "s":
            {		// second
                dt.setSeconds(dt.getSeconds() + p_Number);
                break;
            }
        case "ms":
            {		// second
                dt.setMilliseconds(dt.getMilliseconds() + p_Number);
                break;
            }
        default:
            {
                return "invalid interval: '" + p_Interval + "'";
            }
    }
    return dt;
}



// REQUIRES: isDate()
// NOT SUPPORTED: firstdayofweek and firstweekofyear (defaults for both)
function dateDiff(p_Interval, p_Date1, p_Date2, p_firstdayofweek, p_firstweekofyear) {
    if (!isDate(p_Date1)) { return "invalid date: '" + p_Date1 + "'"; }
    if (!isDate(p_Date2)) { return "invalid date: '" + p_Date2 + "'"; }
    var dt1 = new Date(p_Date1);
    var dt2 = new Date(p_Date2);

    // get ms between dates (UTC) and make into "difference" date
    var iDiffMS = dt2.valueOf() - dt1.valueOf();
    var dtDiff = new Date(iDiffMS);

    // calc various diffs
    var nYears = dt2.getUTCFullYear() - dt1.getUTCFullYear();
    var nMonths = dt2.getUTCMonth() - dt1.getUTCMonth() + (nYears != 0 ? nYears * 12 : 0);
    var nQuarters = Math.round(nMonths / 3); //<<-- different than VBScript, which watches rollover not completion

    var nMilliseconds = iDiffMS;
    var nSeconds = Math.round(iDiffMS / 1000);
    var nMinutes = Math.round(nSeconds / 60);
    var nHours = Math.round(nMinutes / 60);
    var nDays = Math.round(nHours / 24);
    var nWeeks = Math.round(nDays / 7);

    // return requested difference
    var iDiff = 0;
    switch (p_Interval.toLowerCase()) {
        case "yyyy": return nYears;
        case "q": return nQuarters;
        case "m": return nMonths;
        case "y": 		// day of year
        case "d": return nDays;
        case "w": return nDays;
        case "ww": return nWeeks; 	// week of year	// <-- inaccurate, WW should count calendar weeks (# of sundays) between
        case "h": return nHours;
        case "n": return nMinutes;
        case "s": return nSeconds;
        case "ms": return nMilliseconds; // millisecond	// <-- extension for JS, NOT available in VBScript
        default: return "invalid interval: '" + p_Interval + "'";
    }
}



// REQUIRES: isDate(), dateDiff()
// NOT SUPPORTED: firstdayofweek and firstweekofyear (does system default for both)
function datePart(p_Interval, p_Date, p_firstdayofweek, p_firstweekofyear) {
    if (!isDate(p_Date)) { return "invalid date: '" + p_Date + "'"; }

    var dtPart = new Date(p_Date);
    switch (p_Interval.toLowerCase()) {
        case "yyyy": return dtPart.getFullYear();
        case "q": return parseInt(dtPart.getMonth() / 3) + 1;
        case "m": return dtPart.getMonth() + 1;
        case "y": return dateDiff("y", "1/1/" + dtPart.getFullYear(), dtPart); 		// day of year
        case "d": return dtPart.getDate();
        case "w": return dtPart.getDay(); // weekday
        case "ww": return dateDiff("ww", "1/1/" + dtPart.getFullYear(), dtPart); 	// week of year
        case "h": return dtPart.getHours();
        case "n": return dtPart.getMinutes();
        case "s": return dtPart.getSeconds();
        case "ms": return dtPart.getMilliseconds(); // millisecond	// <-- extension for JS, NOT available in VBScript
        default: return "invalid interval: '" + p_Interval + "'";
    }
}


// ====================================

// bootstrap different capitalizations
function IsDate(p_Expression) {
    return isDate(p_Expression);
}
function DateAdd(p_Interval, p_Number, p_Date) {
    return dateAdd(p_Interval, p_Number, p_Date);
}
function DateDiff(p_interval, p_date1, p_date2, p_firstdayofweek, p_firstweekofyear) {
    return dateDiff(p_interval, p_date1, p_date2, p_firstdayofweek, p_firstweekofyear);
}
function DatePart(p_Interval, p_Date, p_firstdayofweek, p_firstweekofyear) {
    return datePart(p_Interval, p_Date, p_firstdayofweek, p_firstweekofyear);
}
function WeekdayName(p_Date) {
    return weekdayName(p_Date);
}
function MonthName(p_Date) {
    return monthName(p_Date);
}

/*
**************************************
* String.mask Function v1.0          *
* Autor: Carlos R. L. Rodrigues      *
**************************************
*/

function dtconv(dtvalue) {
    var months = new Array(12);
    months[0] = "Jan";
    months[1] = "Feb";
    months[2] = "Mar";
    months[3] = "Apr";
    months[4] = "May";
    months[5] = "Jun";
    months[6] = "Jul";
    months[7] = "Aug";
    months[8] = "Sep";
    months[9] = "Oct";
    months[10] = "Nov";
    months[11] = "Dec";

    var mDT = new Date(dtvalue.replace(/\D/g, '/'));
    var mST = "th";
    if (mDT.getDate() == 1 || mDT.getDate() == 21 || mDT.getDate() == 31) {
        mST = "st";
    } else if (mDT.getDate() == 2 || mDT.getDate() == 22) {
        mST = "nd";
    } else if (mDT.getDate() == 3 || mDT.getDate() == 23) {
        mST = "rd";
    }
    return mDT.getDate() + mST + " " + months[mDT.getMonth()] + " " + mDT.getFullYear();
}
function getarrayelm(parray, pid) {
    try {
        if (parray.length - 1 >= pid) {
            return parray[pid];
        } else {
            return "";
        }
    } catch (e) {
        return "";
    }
}
function validchar(strvalue) {
    var mchars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
    for (var i = 0; i < strvalue.length; i++) {
        var char = strvalue.charAt(i);
        if (mchars.indexOf(char) == -1) {
            return false;
        }
    }
    return true;

}
function isError(rData) {
    var rMsg = "";
    if (rData[0] == "err") {
        switch (rData[1]) {
            case "1": rMsg = "Session Expired"; break;
            default: rMsg = rData[1]; break;
        }
    } else if (rData[0] == "nook") {
        rMsg = rData[1];
    } else if (rData[0] != "ok") {
        rMsg = rData[0];
    }
    return rMsg;
}


function isDate2(dateStr) {
    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var matchArray = dateStr.match(datePat); // is the format ok?

    if (matchArray == null) {
        return "Please enter date in proper format";
    }

    month = matchArray[1]; // parse date into variables
    day = matchArray[3];
    year = matchArray[5];
    if (month < 1 || month > 12) { // check month range
        return "Month must be between 1 and 12.";
    }

    if (day < 1 || day > 31) {
        return "Day must be between 1 and 31.";
    }

    if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
        return "Month " + month + " doesn't have 31 days!";
    }

    if (month == 2) { // check for february 29th
        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        if (day > 29 || (day == 29 && !isleap)) {
            return "February " + year + " doesn't have " + day + " days!";
        }
    }
    return ""; // date is valid
}
function chksplchar(fld) {
    for (var i = 0; i < fld.length; i++) {
        if ("!@#$%^&*()_+=|}{\":?><~`';/.,\\][".indexOf(fld.charAt(i)) != -1) { alert("Special characters are not allowed"); return false; }
    }
    return true;
}
function chksplchar2_v1(fld, omit) {
    var spl = "";
    var splchars = "!@#$%^&*()_+=|}{\":?><~`';/.,\\][";
    splchars = splchars.replace(new RegExp('\\' + omit, 'g'), '')
    for (var i = 0; i < fld.value.length; i++) {
        if (splchars.indexOf(fld.value.charAt(i)) != -1) { spl += fld.value.charAt(i); }
    }
    if (spl != "") {
        try {
            for (i = 0; i < spl.length; i++) { fld.value = fld.value.replace(new RegExp('\\' + spl.charAt(i), 'g'), ''); }
            fld.focus();
            return false;
        } catch (e) { }
    }
    return true;
}
function chksplchar2(fld, omit) {
    var spl = "";
    var splchars = "!@#$%^&*()_+=|}{\":?><~`';/.,\\][";
    var arromit = omit.split("");
    for (var msplint = 0; msplint < arromit.length; msplint++) {
        splchars = splchars.replace(new RegExp('\\' + arromit[msplint], 'g'), '')
    }
    for (var i = 0; i < fld.value.length; i++) {
        if (splchars.indexOf(fld.value.charAt(i)) != -1) { spl += fld.value.charAt(i); }
    }
    if (spl != "") {
        try {
            for (i = 0; i < spl.length; i++) { fld.value = fld.value.replace(new RegExp('\\' + spl.charAt(i), 'g'), ''); }
            fld.focus();
            return false;
        } catch (e) { }
    }
    return true;
}

function f_open_window_max(aURL, aWinName) {
    var wOpen;
    var sOptions;

    sOptions = 'status=yes,menubar=yes,scrollbars=yes,resizable=yes,toolbar=yes';
    sOptions = sOptions + ',width=' + (screen.availWidth - 10).toString();
    sOptions = sOptions + ',height=' + (screen.availHeight - 122).toString();
    sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';

    wOpen = window.open('', aWinName, sOptions);
    wOpen.location = aURL;
    try {
        wOpen.focus();
        wOpen.moveTo(0, 0);
        wOpen.resizeTo(screen.availWidth, screen.availHeight);
    } catch (e) { }
    return wOpen;
}

function f_open_window(aURL, aWinName, aWidth, aHeight) {
    var wOpen;
    var sOptions;
    sOptions = 'status=no,menubar=no,scrollbars=yes,resizable=yes,toolbar=no';
    sOptions = sOptions + ',width=' + (screen.availWidth - aWidth).toString();
    sOptions = sOptions + ',height=' + (screen.availHeight - aHeight).toString();
    sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';
    wOpen = window.open('', aWinName, sOptions);
    wOpen.location = aURL;
    try {
        wOpen.focus();
        wOpen.moveTo(0, 0);
        wOpen.resizeTo(screen.availWidth - aWidth, screen.availHeight - aHeight);
    } catch (e) { }
    return wOpen;
}
function roundNumber(rnum, rlength) {
    return Math.round(rnum * Math.pow(10, rlength)) / Math.pow(10, rlength);
}

function isNumber(x) {
    return ((typeof x === typeof 1) && (null !== x) && isFinite(x));
}

function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA	-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
function browser() {
    var cb_ie = /MSIE/.test(navigator.userAgent) || /Trident/.test(navigator.userAgent);
    var cb_moz = !cb_ie && navigator.product == "Gecko";
    return { ie: cb_ie, moz: cb_moz }
}

