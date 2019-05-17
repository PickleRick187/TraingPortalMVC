
var tabSignUpButton = document.getElementById("tab-2");
//var tabSignInButton = document.getElementById("tab-1");


function displaySignUp() {
    var signUpPage = document.getElementById("show-SignUp");
    signUpPage.style.display = 'block'
}

function hideSignUp() {
    var signUpPage = document.getElementById("show-SignUp");
    signUpPage.style.display = 'none';
  
}

tabSignUpButton.addEventListener("click", displaySignUp, false);

tabSignInButton.addEventListener("click", hideSignUp, false);


$(function () {
    if ($('.nav>ul>li').hasClass('selected')) {
        $('.selected').addClass('active');
        var currentleft = $('.selected').position().left + "px";
        var currentwidth = $('.selected').css('width');
        $('.lamp').css({ "left": currentleft, "width": currentwidth });
    }
    else {
        $('.nav>ul>li').first().addClass('active');
        var currentleft = $('.active').position().left + "px";
        var currentwidth = $('.active').css('width');
        $('.lamp').css({ "left": currentleft, "width": currentwidth });
    }
    $('.nav>ul>li').hover(function () {
        $('.nav ul li').removeClass('active');
        $(this).addClass('active');
        var currentleft = $('.active').position().left + "px";
        var currentwidth = $('.active').css('width');
        $('.lamp').css({ "left": currentleft, "width": currentwidth });
    }, function () {
        if ($('.nav>ul>li').hasClass('selected')) {
            $('.selected').addClass('active');
            var currentleft = $('.selected').position().left + "px";
            var currentwidth = $('.selected').css('width');
            $('.lamp').css({ "left": currentleft, "width": currentwidth });
        }
        else {
            $('.nav>ul>li').first().addClass('active');
            var currentleft = $('.active').position().left + "px";
            var currentwidth = $('.active').css('width');
            $('.lamp').css({ "left": currentleft, "width": currentwidth });
        }
    });
});
