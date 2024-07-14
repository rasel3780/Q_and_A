var UserController = {
    GetUser: () => {
        showLoading();
        var userName = $('#userName').val();
        var password = $('#password').val();

        var user = {
            UserName: userName,
            Password: password
        };

        console.log("Sending user from controller:", JSON.stringify(user));

        UserService.VerifyUser(user, (response) => {
            hideLoading();
            if (response) {
                NotificationHelper.showSuccess('User authenticated successfully!', () => {
                    localStorage.setItem('token', response.token);
                    localStorage.setItem('userID', response.userID);
                    localStorage.setItem('userName', response.userName);
                    var urlParams = new URLSearchParams(window.location.search);
                    var redirectTo = urlParams.get('redirectTo');
                    if (redirectTo === 'askQuestion') {
                        window.location.href = '/Question/AskQuestion';
                    } else {
                        window.location.href = '/';
                    }
                });
            } else {
                NotificationHelper.showError('User name or password did not match. Please try again.');
            }
        });
    },

    RegisterUser: () => {
        showLoading();
        var userName = $('#userName').val();
        var email = $('#email').val();
        var password = $('#password').val();

        var user = {
            UserName: userName,
            Email: email,
            Password: password
        };

        UserService.PostUser(user, (response, error) => {
            hideLoading();
            if (response && response.success) {
                NotificationHelper.showSuccess('User registered successfully! Login to continue', () => {
                    window.location.href = '/User/Login';
                });
            } else {
                NotificationHelper.showError('Registration failed: ' + (error.message || 'Please try again.'));
            }
        });
    },

    GetQuestionListByUser: (userID) => {
        showLoading();
        console.log("Get Question List by user called in controller:" + userID);
     
        UserService.GetQuestionByUser(userID, function (data) {
            const questionList = $("#questionList");
            questionList.empty();

            if (data.length === 0) {
                questionList.append("<tr><td colspan='3'>No questions found</td></tr>");
            } else {
                data.forEach(question => {
                    const row = `<tr>
                                    <td><a href="/Question/Detail/${question.questionID}" class="text-primary">${question.title}</a></td>Question/Detail/${question.questionID}'">${question.title}</td>
                                    <td>${question.category}</td>
                                    <td>${new Date(question.makeDate).toLocaleDateString()}</td>
                                 </tr>`;
                    questionList.append(row);
                });
                
            }
            hideLoading();
        });
    }

}