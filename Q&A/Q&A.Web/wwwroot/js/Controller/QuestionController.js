var QuestionController = {
    LstQuestions: () => {
        QuestionService.QuestionLst(response => {
            if (response) {
                let questionContent = '';
                $.each(response, function (index, value) {
                    let truncatedQuestionText = value.questionText.length > 25 ? value.questionText.substring(0, 25) + '... <a href="/Question/Detail/' + value.questionID + '" class="see-details">see details</a>' : value.questionText;
                    questionContent += `
                        <div class="col-12 mb-3">
                            <div class="card ques-card" >
                                <div class="card-body ques-card-body d-flex">
                                    <div class="stats mr-3 text-center">
                                        <div class="answers mt-2">
                                            <span class="badge bg-secondary">${value.answerCount}</span>
                                            <div>answers</div>
                                        </div>
                                    </div>
                                    <div class="question-details flex-grow-1">
                                        <h5 class="card-title text-primary" style="cursor: pointer;" onclick="window.location.href='/Question/Detail/${value.questionID}'">
                                            <u>${value.title}</u>
                                        </h5>
                                        <p class="card-text">${truncatedQuestionText}</p>
                                        <div class="d-flex justify-content-between align-items-center mt-4">
                                            <small>Category:
                                                <span class="badge bg-secondary">${value.category}</span>
                                            </small>
                                            <small class="text-muted">Asked by:
                                                <span class="text-primary">${value.makeBy}</span> 
                                                on ${new Date(value.makeDate).toLocaleDateString()}
                                            </small>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `;
                });
                $('#questionContainer').html(questionContent);

            }
            else {
                $('#questionContainer').html('<p>No questions available to show at this moment.</p>');
            }

        });
    },

    LoadQuestionDetail: (questionID) => {
        console.log("Load Details called", questionID);
        QuestionService.GetQuestionDetail(questionID, response => {
            if (response) {
                let codeSnippetContent = response.codeSnippet ? `<pre><code class="language-javascript">${response.codeSnippet}</code></pre>` : '';
                let questionContent = `
                <h3 class="text-primary">${response.title}</h3>
                <hr>
                <p>${response.questionText}</p>
                ${codeSnippetContent}
                <div class="d-flex justify-content-between align-items-center mt-4">
                    <small>Category:
                        <span class="badge bg-secondary">${response.category}</span>
                    </small>
                    <small class="text-muted">Asked by:
                        <span class="text-primary">${response.makeBy}</span> 
                        on ${new Date(response.makeDate).toLocaleDateString()}
                    </small>
                </div>
            `;
                $('#questionDetail').html(questionContent);

                // Highlight code snippets
                document.querySelectorAll('pre code').forEach((block) => {
                    hljs.highlightBlock(block);
                });

                // Load answers
                if (response.answersList && response.answersList.length > 0) {
                    AnswerController.LoadAnswer(response.answersList);
                } else {
                    $('.answerContainer').html('<h2>No answers available for this question yet</h2>');
                }
            } else {
                $('#questionDetail').html('<p>Question not found.</p>');
            }
        });
    },

    PostQuestion: () => {

        var token = localStorage.getItem('token');
        var userName = localStorage.getItem('userName');

        var title = $('#title').val();
        var category = $('#category').val();
        var questionText = $('#questionText').val();
        var codeSnippet = $('#codeSnippet').val();
        var makeByUserId = localStorage.getItem('userID');
        
        console.log(makeByUserId);
        var question = {
            Title: title,
            Category: category,
            QuestionText: questionText,
            CodeSnippet: codeSnippet,
            MakeByUserId: makeByUserId,
            MakeBy: userName,
            MakeDate: new Date().toISOString()
        };

        QuestionService.PostQuestion(question, response => {
            if (response) {
                alert('Question posted successfully!');
                window.location.href = '/Question/';
            } else {
                alert('Failed to post the question. Please try again.');
            }
        });
    },
 

};
$(document).ready(function () {
    $('#postQuestionForm').on('submit', function (event) {
        event.preventDefault();
        QuestionController.PostQuestion();
    });
});
