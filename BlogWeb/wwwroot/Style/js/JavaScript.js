// JavaScript.js

document.addEventListener('DOMContentLoaded', function () {
    const submitBtn = document.querySelector('.submit_btn');

    if (submitBtn) {
        submitBtn.addEventListener('click', function (e) {
            e.preventDefault();

            // Lấy thông tin từ form
            const name = document.getElementById('name').value;
            const email = document.getElementById('email').value;
            const subject = document.getElementById('subject').value;
            const message = document.querySelector('textarea[name="message"]').value;

            // Kiểm tra thông tin đã đầy đủ chưa
            if (name && email && message) {
                // Tạo đối tượng comment
                const comment = {
                    name: name,
                    email: email,
                    subject: subject,
                    message: message,
                    date: new Date().toLocaleString()
                };

                // Lấy danh sách comment hiện tại từ cookie
                let comments = JSON.parse(getCookie('comments') || '[]');

                // Thêm comment mới vào danh sách
                comments.push(comment);

                // Lưu lại vào cookie
                setCookie('comments', JSON.stringify(comments), 7);

                // Hiển thị comment mới
                displayComments();

                // Xóa form sau khi gửi
                document.querySelector('form').reset();
            } else {
                alert('Please fill out all required fields.');
            }
        });
    }

    // Hàm lưu cookie
    function setCookie(name, value, days) {
        let expires = "";
        if (days) {
            const date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }

    // Hàm lấy cookie
    function getCookie(name) {
        const nameEQ = name + "=";
        const ca = document.cookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    // Hiển thị comment từ cookie
    function displayComments() {
        const commentList = document.querySelector('.comment-list');
        if (commentList) {
            commentList.innerHTML = ''; // Xóa danh sách hiện tại
            const comments = JSON.parse(getCookie('comments') || '[]');

            comments.forEach(comment => {
                commentList.innerHTML += `
                    <div class="single-comment justify-content-between d-flex">
                        <div class="user justify-content-between d-flex">
                            <div class="desc">
                                <h5><a>${comment.name}</a></h5>
                                <p class="date">${comment.date}</p>
                                <p class="comment">
                                    ${comment.message}
                                </p>
                            </div>
                        </div>

                    </div>
                `;
            });
        }
    }

    // Hiển thị comment khi trang được tải
    displayComments();
});
