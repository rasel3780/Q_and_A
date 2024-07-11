var AnswerController = {
    LoadAnswer: (answers) => {
        console.log("LoadAnswer in AnsController called");
        let answerContent = '<h3 style="text-align:center">Answers</h3><ul>';

        $.each(answers, function (index, value) {
            answerContent += `
                <li>
                    <p>${value.answerText}</p>
                    ${value.codeSnippet ? `<pre><code>${value.codeSnippet}</code></pre>` : ''}
                    <p><strong>Answered by:</strong> ${value.makeBy} on ${new Date(value.makeDate).toLocaleDateString()}</p>
                    ${value.answerAcceptedBy ? `<p><strong>Accepted by:</strong> ${value.answerAcceptedBy} on ${new Date(value.acceptedDate).toLocaleDateString()}</p>` : ''}
                </li>
                <hr>
            `;
        });
        answerContent += '</ul>';
        $('.answerContainer').html(answerContent);
        $('pre code').each(function (i, block) {
            hljs.highlightBlock(block);
        });
    },
    PostAnswer: () => {
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
            if (response) {
                alert('Answer posted successfully!');
                
                
                QuestionController.LoadQuestionDetail(questionId);
                $('#AnswerText').val('');
                $('#CodeSnippet').val('');
            } else {
                alert('Failed to post the answer. Please try again.');
            }
        });
    }


}