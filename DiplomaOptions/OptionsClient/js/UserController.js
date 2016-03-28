(function () {

    var app = angular.module("diplomaPicker");
    var baseUrl = "http://optionswebapi.rickychen.me/api/";

    var CreateUser = function ($scope, $http, $location) {
        $scope.register = function (user) {
            var username, email, password, confirmpass;
            var url     = baseUrl.concat("Account/Register");
            username    = user.userName;
            email       = user.email;
            password    = user.password;
            confirmpass = user.confirmPassword;
            //console.log(email);
            $("#username-error").hide();
            $("#email-error").hide();
            $("#password-error").hide();
            $("#confirmpassword-error").hide();

            var param = {
                UserName:           username,
                Email:              email,
                Password:           password,
                ConfirmPassword:    confirmpass
            };

            var result = validate(param);
            var isValid = true;
            if (result.Username != null) {
                $scope.username_valid = result.Username;
                $("#username-error").show();
                isValid = false;
            }

            if (result.Email != null) {
                isValid = false;
                $("#email-error").show();
                $scope.email_valid = result.Email;
            }

            if (result.Password != null) {
                isValid = false;
                $("#password-error").show();
                $scope.password_valid = result.Password;
            }

            if (result.ConfirmPassword != null) {
                isValid = false;
                $("#confirmpassword-error").show();
                $scope.confirmpassword_valid = result.ConfirmPassword;
            }
            
            if (!isValid) {
                //$location.path("/view/register.html");
                window.alert("something's wrong");
            } else {
                $location.path("/pick");
                setCookie("username", username, 1);
                //window.alert(getCookie("username"));
                //console.log(JSON.stringify(param));
                $('#loadingmessage').show();
                $.ajax({
                    url: url,
                    type: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    data: JSON.stringify(param)
                }).done(function (data) {
                    console.log("success" + data);
                    $('#loadingmessage').hide();
                    $('#success').show();
                    console.log("success");
                }).fail(function (err) {
                    console.warn(err);
                    $('#loadingmessage').hide();
                    $('#fail').show();
                    console.log("fail");
                });
            }
        };
    }

    app.controller("CreateUser", CreateUser);
}());
(function () {

    var app = angular.module("diplomaPicker");

    var Login = function ($scope, $http) {
        $scope.signin = function () {
            var username, password;
            username = $("#username").val();
            password = $("#pwd").val();

            var param = {
                grant_type: "password",
                Username: username,
                Password: password
            };
        };
    }

    app.controller("Login", Login);
}());

(function () {

    var app = angular.module("diplomaPicker");
    
    var Logout = function ($scope, $http, $location) {
        setCookie("username", 0, -1);
        document.cookie = null;
        $location.path("/login");
        /*
        $scope.logout = function () {
            window.alert("test");
            setCookie("username", 0, -1);
            $location.path("/login");
        };*/
    }
    app.controller("Logout", Logout);
}());

function validate(param) {
    var id_regex = new RegExp("^(A00[0-9]{6,6})$");
    var email_regex = new RegExp("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
    var password_regex = new RegExp("^(?=(.*\\d))(?=(.*\\W)).{7,}")
    console.log(email_regex.exec(param.Email));

    var result = {
        Username: null,
        Email: null,
        Password: null,
        ConfirmPassword: null
    };

    if (id_regex.exec(param.UserName) == null) {
        console.log(id_regex.exec("username " + param.UserName));
        result.Username = "Empty or Invalid Username, Please use Student Id";
    }
    if (email_regex.exec(param.Email) == null) {
        console.log(email_regex.exec("email " + param.Email));
        result.Email = "Empty or Invalid Email";
    }

    if (password_regex.exec(param.Password) == null && param.Password != null) {
        result.Password = "Password must be 7 characters long, and contain at least one number and one special character.";
    }

    if ((param.Password != param.ConfirmPassword) && param.ConfirmPassword != null) {
        result.ConfirmPassword = "Password did not match";
    }

    return result;
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    var expires = "expires="+d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i=0; i<ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

function checkCookie() {
    var user = getCookie("username");
    if (user != "") {
        return false;
    } else {
        return true;
    }
}

