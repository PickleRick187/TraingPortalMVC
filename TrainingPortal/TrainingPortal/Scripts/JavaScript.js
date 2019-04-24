
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



