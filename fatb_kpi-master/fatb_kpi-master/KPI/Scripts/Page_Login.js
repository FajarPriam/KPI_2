$(document).ready(function () {
    getRole();
})

function getRole() {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "getRole",
        success: function (result) {
            if (result.Status) {
                var l = result.Data;
                $("#role").append(new Option('Role', 0));
                $.each(l, function (key, item) {
                    $("#role").append(new Option(item.ROLE, item.ID));
                })
            }
            else {
                alert(result.Error);
            }
        }
    })
}

function login() {
    var obj = {
        user: $("#nrp").val()
        , role: $("#role").val()
        , pass: $("#password").val()
    }


    var spinner = `<span class="spinner-border spinner-border-md" role="status" aria-hidden="true"></span> Loading...`;
    $('#btn-login').prop('disabled', true);
    $('#btn-login').html(spinner);

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "login",
        data: JSON.stringify(obj),
        success: function (result) {
            if (result.Status == true) {
                console.log(result.StatusLogin);
                if (result.StatusLogin == 1) {
                    getSession();
                } else if (result.StatusLogin == 2) {
                    alert("user tidak terdaftar dengan role tersebut!!");
                } else {
                    alert("username/password salah!!");
                }
            } else {
                alert(result.Error);
            }


            var spinner = `Log in`;
            $('#btn-login').prop('disabled', false);
            $('#btn-login').html(spinner);
        }
    })
}

function getSession() {
    var obj = {
        user: $("#nrp").val()
        , role: $("#role").val()
        , pass: $("#password").val()
    }

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: $("#hd_path").val() + "getSession",
        data: JSON.stringify(obj),
        success: function (result) {
            if (result.Status == true) {
                setSession(result.Data);
            } else {
                alert(result.Error);
            }
        }
    })
}

function setSession(data) {
    console.log('masuk set sesion');

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "/Login/setSession",
        data: JSON.stringify(data),
        success: function (result) {
            window.location.href = "/Home/Index";
        }
    })
}

