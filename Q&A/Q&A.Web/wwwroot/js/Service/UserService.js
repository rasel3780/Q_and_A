var UserService = {

    PostUser: (user, callback) => {
        console.log("user service called");
        console.log("Sending question:", JSON.stringify(user));
        $.post({
            url: 'http://localhost:5207/api/Auth/Login',
            contentType: 'application/json',
            data: JSON.stringify(user),
            success: function (response) {
                console.log("Success response:", response);
                callback(response);
            }
            
        });
    }
}