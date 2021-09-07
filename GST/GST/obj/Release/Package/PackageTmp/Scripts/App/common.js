$(document).ready(function () {
   
    $('.newpatientfilter-menu a').click(function(){
      
        $('.listfiltertxt').html($('this').html());
        
    });



 $(".search").click(function(){
        $(".gsearch").fadeIn("slow").focus();
        $(this).hide();
        $(".searchclose").fadeIn("slow");
       
    });
 $(".searchclose").click(function(){
        $(".gsearch").fadeOut("slow");
        $(this).hide();
        $(".search").fadeIn("slow");
       
    });
    
    
$('.newselect').select2();
 

$(".parent-tab a").click(function() {
  var position = $(this).parent().position();
  var width = $(this).parent().width();
    $(".slider").css({"left":+ position.left,"width":width});
    

    setTimeout(function(){ 
        var actWidth2 = $(".child-tab").find(".active").parent("li").width();
        var actPosition2 = $(".child-tab .active").position();
        $(".slider2").css({"left":+ actPosition2.left,"width": actWidth2});
    }, 200);

});
var actWidth = $(".parent-tab").find(".active").parent("li").width();
var actPosition = $(".parent-tab .active").position();
$(".slider").css({"left":+ actPosition.left,"width": actWidth});


    
//Sub tab
    
$(".child-tab a").click(function() {
   
  var position2 = $(this).parent().position();
  var width2 = $(this).parent().width();

    $(".slider2").css({"left":+ position2.left,"width":width2});
    
});


   var formsliderw = $(".formslider").width();
        $(".slide-left").click(function(){
            $(".formslider").animate({
                width: 0,
                right:'-9999px'
            });
            $('.freezelayer').addClass('no');
        });
    
        $(".slide-right").click(function(){
            $(".formslider").animate({
                width: formsliderw,
                right:0
            });
            $('.freezelayer').removeClass('no');
            
        });
    
//document ready close
});

