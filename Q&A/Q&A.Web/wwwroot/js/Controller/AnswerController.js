var AnswerController = {
    LoadAnswer: (questionId) => {
        console.log("LoadAnswer in AnsController called");
        AnswerService.GetAnswerByQuestionId(questionId, function (response) {
            let answerContent = '<h2>Answers</h2><ul>';
            console.log(response.length);
            if (response.length <= 0) {
                answerContent = `<h2>No answer avilable for this question yet</h2>`
            }
            $.each(response, function (index, value) {
                answerContent += `
                        <li>
                            <p>${value.answerText}</p>
                            ${value.codeSnippet ? `<pre><code>${value.codeSnippet}</code></pre>` : ''}
                            <p><strong>Answered by:</strong> ${value.makeBy} on ${new Date(value.makeDate).toLocaleDateString()}</p>
                            ${value.answerAcceptedBy ? `<p><strong>Accepted by:</strong> ${value.answerAcceptedBy} on ${new Date(value.answerAcceptedDate).toLocaleDateString()}</p>` : ''}
                        </li>
                        <hr>
                    `;
            });
            answerContent += '</ul><hr>';
            $('.answerContainer').html(answerContent);
            $('pre code').each(function (i, block) {
                hljs.highlightBlock(block);
            });
        });
    }
}