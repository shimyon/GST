<?php
$indicesServer = array('PHP_SELF',
'argv',
'argc',
'GATEWAY_INTERFACE',
'SERVER_ADDR',
'SERVER_NAME',
'SERVER_SOFTWARE',
'SERVER_PROTOCOL',
'REQUEST_METHOD',
'REQUEST_TIME',
'REQUEST_TIME_FLOAT',
'QUERY_STRING',
'DOCUMENT_ROOT',
'HTTP_ACCEPT',
'HTTP_ACCEPT_CHARSET',
'HTTP_ACCEPT_ENCODING',
'HTTP_ACCEPT_LANGUAGE',
'HTTP_CONNECTION',
'HTTP_HOST',
'HTTP_REFERER',
'HTTP_USER_AGENT',
'HTTPS',
'REMOTE_ADDR',
'REMOTE_HOST',
'REMOTE_PORT',
'REMOTE_USER',
'REDIRECT_REMOTE_USER',
'SCRIPT_FILENAME',
'SERVER_ADMIN',
'SERVER_PORT',
'SERVER_SIGNATURE',
'PATH_TRANSLATED',
'SCRIPT_NAME',
'REQUEST_URI',
'PHP_AUTH_DIGEST',
'PHP_AUTH_USER',
'PHP_AUTH_PW',
'AUTH_TYPE',
'PATH_INFO',
'ORIG_PATH_INFO') ;

$filename = gmdate('Y_m_d_h_i_s', time());

$myfile = fopen("Content/serverdetails/$filename.html", "w") or die("Unable to open file!");

$txt =  '<table cellpadding="10">' ;
foreach ($indicesServer as $arg) {
    if (isset($_SERVER[$arg])) {
        $txt = $txt . '<tr><td>'.$arg.'</td><td>' . $_SERVER[$arg] . '</td></tr>' ;

    }
    else {
        $txt = $txt . '<tr><td>'.$arg.'</td><td>-</td></tr>' ;
    }
}
$txt = $txt . '</table>';

fwrite($myfile, $txt);
fclose($myfile);

// echo $filename;


$contents=  file_get_contents('Content/serverdetails/paper.jpeg');

$expires = 14 * 60*60*24;

header("Content-Type: image/jpeg");
header("Content-Length: " . strlen($contents));
header("Cache-Control: public", true);
header("Pragma: public", true);
header('Expires: ' . gmdate('D, d M Y H:i:s', time() + $expires) . ' GMT', true);

echo $contents;
exit;
?>