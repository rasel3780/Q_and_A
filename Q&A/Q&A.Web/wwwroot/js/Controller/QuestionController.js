var QuestionController = {
    LstQuestions: () => {
        QuestionService.QuestionLst(response => {
            if (response) {
                let questionContent = '';
                $.each(response, function (index, value) {
                    questionContent += `
                        <div class="col-12 mb-3">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title text-primary" style="cursor: pointer;"      onclick="window.location.href='/Question/Detail/${value.questionID}'" >
                                        ${value.title}
                                    </h5>
                                   
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
                    <div class="d-flex justify-content-between">
                        <span class="badge bg-secondary">${response.category}</span>
                        <small class="text-primary">${response.makeBy}</small>
                        <small>${new Date(response.makeDate).toLocaleDateString()}</small>
                    </div>
                `;
                $('#questionDetail').html(questionContent);
           
                document.querySelectorAll('pre code').forEach((block) => {
                    hljs.highlightBlock(block);
                });
            } else {
                $('#questionDetail').html('<p>Question not found.</p>');
            }
        });
        AnswerController.LoadAnswer(questionID);
    },
}
