var QuestionController = {
    LstQuestion: ()=> {
        QuestionService.QuestionLst(function (response) {
            let questionContent = '';
            $.each(response, function (index, value) {
                questionContent += `
                  <div class="col-12 mb-3">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title text-primary" style="cursor: pointer;" onclick="window.location.href='/Question/Detail/${value.questionID}'" >${value.title}</h5>
                                <p class="card-text">${value.question}</p>
                                <div class="d-flex justify-content-between align-items-center mt-4">
                                    <small>Category: <span class="badge bg-secondary">${value.category}</span></small>
                                    <small class="text-muted">Asked by:  <span class="text-primary">${value.makeBy}</span>  on ${new Date(value.makeDate).toLocaleDateString()}</small>
                                </div>
                            </div>
                        </div>
                    </div>
                `;
            });
            $('#questionContainer').html(questionContent);
        });
    },
    LoadQuestionDetail: (questionID) => {
        QuestionService.GetQuestionDetail(questionID, function (question) {
            let questionContent = `
                <h3 class="text-primary">${question.title}</h3>
                <p class="text-muted">#${question.questionID}</p>
                <p>${question.question}</p>
                <div class="d-flex justify-content-between">
                    <span class="badge bg-secondary">${question.category}</span>
                    <small class="text-primary">${question.makeBy}</small>
                    <small>${new Date(question.makeDate).toLocaleDateString()}</small>
                </div>
            `;
            $('#questionDetail').html(questionContent);

            
        });
    }

};
