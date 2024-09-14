const magicBall = document.getElementById("mb-container");
const response = document.getElementById("response");
const questionInput = document.getElementById("question-input");

// Добавляем обработчик клика
magicBall.addEventListener("click", async function () {
    // Устанавливаем класс для анимации
    magicBall.className = 'magicball-outer animate';

    const question = questionInput.value.trim(); // Получаем значение из поля ввода

    questionInput.value = ''; // Очистка input
    // Отправляем запрос к API
    try {
        const res = await fetch("/api/magicball/GetAnswer", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ question }) // Отправляем вопрос как JSON
        });

        if (!res.ok) {
            throw new Error("Ошибка при получении ответа от сервера");
        }

        // Получаем ответ
        const data = await res.json();
        const message = data; // Предполагаем, что ответ - это текст

        // Устанавливаем текст ответа через 3 секунды
        setTimeout(() => {
            response.textContent = message;
            response.style.fontSize = '1.1rem';

            // Убираем класс анимации
            magicBall.className = 'magicball-outer';

            
        }, 3000);
    } catch (error) {
        console.error("Ошибка:", error);
        response.textContent = "Произошла ошибка при получении ответа!";
        magicBall.className = 'magicball-outer';
    }
});