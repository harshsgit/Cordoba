/*********************************************
*                ON DOM READY                *
*********************************************/
$(function() {
    $('.lk-toggle-nav').click(function() {
        toggleNav();
    });
    $('.lk-close').click(function() {
        toggleNav();
    });
});

function toggleNav() {
    if($('.lk-wrapper').hasClass('show-nav')) {
        $('.lk-wrapper').removeClass('show-nav');
    }
    else {
        $('.lk-wrapper').addClass('show-nav');
    }
}


$(function(){
    $(".lk-wrapper").css("width", "90px");
     $(".lk-wrapper").css("height", "800px");

});

$(".lk-toggle-nav").click(function(){
    $(".lk-content").css("top", "0");
});

$(function(){
    $(".lk-wrapper").css("z-index", "0");
});

$(".lk-wrapper").click(function(){
    $(".show-nav").css("z-index", "99999");
     $(".show-nav").css("width", "100%");
});


