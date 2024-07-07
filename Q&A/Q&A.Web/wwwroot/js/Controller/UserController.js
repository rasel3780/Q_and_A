var UserController = {
    
    GetUser: () => {
        var userName = $('#userName').val();
        var password = $('#password').val();
 

        var user = {
            UserName: userName,
            Password: password

        };

        console.log("Sending user from controller:", JSON.stringify(user));

        UserService.PostUser(user, response => {
            if (response) {
                alert('user authenticated successfully!');
                localStorage.setItem('userID', response.userID);
                localStorage.setItem('userName', response.userName);
                window.location.href = '/';
            } else {
                alert('Failed to auth the user. Please try again.');
            }
        });
    }


}