var UserService = {
    VerifyUser: (user, callback) => {
        console.log("user service called");
        console.log("Sending user:", JSON.stringify(user));
        $.post({
            url: 'http://localhost:5207/api/User/Login',  
            contentType: 'application/json',
            data: JSON.stringify(user),
            success: function (response) {
                console.log("Success response:", response);
                callback(response);
            },
            error: function (xhr, status, error) {
                console.log("Error:", error);
                callback(null, error);
            }
        });
    },

    PostUser: (user, callback) => {
        console.log("user service called");
        console.log("Sending user:", JSON.stringify(user));
        $.post({
            url: 'http://localhost:5207/api/User/Register',
            contentType: 'application/json',
            data: JSON.stringify(user),
            success: function (response) {
                console.log("Success response:", response);
                callback(response);
            },
            error: function (xhr, status, error) {
                console.log("Error:", error);
                callback(null, error);
            }
        });
    },

    GetQuestionByUser: (userID, callback) => {
        console.log("Get Question By user service called:" + userID);
        $.ajax({
            url: "http://localhost:5207/api/Question/UserQuestions/" + userID,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            },
            success: function (data) {
                callback(data);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching questions:", error);
                callback([]);
            }
        });
    },

    CheckUnique: (field, value, callback) => {
        $.ajax({
            url: `http://localhost:5207/api/User/CheckUnique?field=${field}&value=${encodeURIComponent(value)}`,
            async: false,
            success: function (response) {
                callback(response.isUnique);
            },
            error: function () {
                callback(false);
            }
        });
    }
}
