function btn_Click() {
    if ($("#code").val() == "") {
        alert('登录工号不能为空！');
        return false;
    };
    if ($("#password").val() == "") {
        alert('密码不能为空！');
        return false;
    };
    $.ajax({
        type: "POST",
        url: '../Account/Login',
        data: $("#formLogin").serialize(),
        cache: false,
        dataType: 'json',
        success: function (data, textStatus) {
            if (data && data.isSuccess) {
                switch (data.model.usr_Dept_Id) {
                    default: location.href = '../Home/Index';
                        break;
                    case 3: location.href = '../BusinessMsg/Index';
                }
                
            } else {
                alert(data.message);
            };
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.info("Ajax 执行失败");
        }
    });
}