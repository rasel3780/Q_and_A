var UserController = {
    GetUser: () => {
        var userName = $('#userName').val();
        var password = $('#password').val();

        var user = {
            UserName: userName,
            Password: password
        };

        console.log("Sending user from controller:", JSON.stringify(user));

        UserService.VerifyUser(user, response => {
            if (response) {
                alert('user authenticated successfully!');
                localStorage.setItem('token', response.token);
                localStorage.setItem('userID', response.userID);
                localStorage.setItem('userName', response.userName);
                // Check if there's a redirect parameter
                var urlParams = new URLSearchParams(window.location.search);
                var redirectTo = urlParams.get('redirectTo');
                if (redirectTo === 'askQuestion') {
                    window.location.href = '/Question/AskQuestion';
                } else {
                    window.location.href = '/';
                }
            } else {
                alert('Failed to authenticate the user. Please try again.');
            }
        });
    },

    RegisterUser: () => {
        var userName = $('#userName').val();
        var email = $('#email').val();
        var password = $('#password').val();

        var user = {
            UserName: userName,
            Email: email,
            Password: password
        };

        UserService.PostUser(user, (response, error) => {
            if (response && response.success) {
                alert('User registered successfully!');
                window.location.href = '/User/Login';
            } else {
                alert('Registration failed: ' + (error.message || 'Please try again.'));
            }
        });
    }
}
