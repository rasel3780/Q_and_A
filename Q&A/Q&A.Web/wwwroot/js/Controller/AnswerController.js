var AnswerController = {
    LoadAnswer: (answers, askedByUserId) => {
        showLoading();
        console.log("LoadAnswer in AnsController called by: " + askedByUserId);

        let answerContent = '<h3 style="text-align:center">Answers</h3>';
        const currentUserId = localStorage.getItem('userID');
        const canAcceptAnswers = currentUserId == askedByUserId;

        $.each(answers, function (index, value) {
            let acceptButton = '';
            let acceptedBadge = '';

            if (canAcceptAnswers && !value.isAccepted) {
                acceptButton = `<button class="btn btn-success btn-sm accept-answer" data-answer-id="${value.answerID}">Accept Answer</button>`;
            }

            if (value.isAccepted) {
                acceptedBadge = '<span class="badge bg-success">Accepted</span>';
            }

            answerContent += `
                <div class="card mb-3">
                    <div class="card-body">
                        <p>${value.answerText}</p>
                        ${value.codeSnippet ? `<pre><code class="language-javascript">${value.codeSnippet}</code></pre>` : ''}
                        <div class="d-flex justify-content-between align-items-center mt-3">
                            <small class="text-muted">Answered by: 
                                <span class="text-primary">${value.makeBy}</span> 
                                on ${new Date(value.makeDate).toLocaleDateString()}
                            </small>
                            <div>
                                ${acceptedBadge}
                                ${acceptButton}
                            </div>
                        </div>
                    </div>
                </div>
            `;
        });

        $('.answerContainer').html(answerContent);

        // Highlight code snippets
        document.querySelectorAll('pre code').forEach((block) => {
            hljs.highlightBlock(block);
        });

        $('.accept-answer').on('click', function () {
            const answerId = $(this).data('answer-id');
            AnswerController.AcceptAnswer(answerId);
        });

        hideLoading();
    },

    PostAnswer: () => {
        showLoading();
        var questionId = $('#QuestionID').val();
        var answerText = $('#AnswerText').val();
        var codeSnippet = $('#CodeSnippet').val();
        var makeByUserId = localStorage.getItem('userID');
        var userName = localStorage.getItem('userName');

        var answer = {
            QuestionID: parseInt(questionId),
            AnswerText: answerText,
            CodeSnippet: codeSnippet,
            MakeByUserID: makeByUserId,
            MakeBy: userName,
            MakeDate: new Date().toISOString()
        };
        console.log("Sending answer:", JSON.stringify(answer));
        AnswerService.PostAnswer(answer, response => {
            hideLoading();
            if (response) {
                NotificationHelper.showSuccess('Answer posted successfully!', () => {
                    QuestionController.LoadQuestionDetail(questionId);
                    $('#AnswerText').val('');
                    $('#CodeSnippet').val('');
                });
            } else {
                NotificationHelper.showError('Failed to post the answer. Please try again.');
            }
        });
    },

    AcceptAnswer: (answerId) => {
        showLoading();
        AnswerService.AcceptAnswer(answerId, (response, error) => {
            hideLoading();
            if (response && response.success) {
                NotificationHelper.showSuccess('Answer accepted successfully!');
                // Reload the question details to reflect the changes
                QuestionController.LoadQuestionDetail($('#QuestionID').val());
            } else {
                NotificationHelper.showError('Failed to accept answer: ' + (error || 'Unknown error'));
            }
        });
    }
};

$(document).ready(function () {
    $('#answerForm').off('submit').on('submit', function (event) {
        event.preventDefault();
        AnswerController.PostAnswer();
    });
});
