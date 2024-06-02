var QuestionAnswerController = {
    LstQuestion: function () {
        QuestionAnswerService.QuestionLst(function (response) {
            let questionContent = '';
            $.each(response, function (index, value) {
                questionContent += `
                  <div class="col-12 mb-3">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title text-primary" style="cursor: pointer;"   >${value.title}</h5>
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
    }
};
