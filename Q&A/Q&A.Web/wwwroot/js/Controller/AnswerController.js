var AnswerController = {
    LoadAnswer: (questionId) => {
        console.log("LoadAnswer in AnsController called");
        AnswerService.GetAnswerByQuestionId(questionId, function (response) {
            let answerContent = '';
            $.each(response, function (index, value) {
                answerContent += `
                  <div class="answer">
                        <p>${value.answer}</p>
                        <div class="d-flex justify-content-between">
                            <small class="text-primary">${value.makeBy}</small>
                            <small>${new Date(value.makeDate).toLocaleDateString()}</small>
                        </div>
                    </div>  
                
                `;
            });
            $('#answerContainer').html(answerContent);
        });
    }
}