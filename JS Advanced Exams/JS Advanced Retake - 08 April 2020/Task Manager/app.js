function solve() {
    // Elements
    let sectionOpen = document.querySelector('section:nth-child(2) div:nth-child(2)');
    let sectionInProgress = document.querySelector('section:nth-child(3) div:nth-child(2)');
    let sectionComplete = document.querySelector('section:nth-child(4) div:nth-child(2)');

    let taskElement = document.getElementById('task');
    let descriptionElement = document.getElementById('description');
    let dateElement = document.getElementById('date');
    let addBtnElement = document.getElementById('add');

    // Creating Tree
    addBtnElement.addEventListener('click', createOpenCourse);

    // Define tree elements 
    function createOpenCourse(event) {
        event.preventDefault();

        // Validating input data

        if (taskElement.value == '' || taskElement.value == null || taskElement.value == undefined) {
            return;
        }

        if (descriptionElement.value == '' || descriptionElement.value == null || descriptionElement.value == undefined) {
            return;
        }

        if (dateElement.value == '' || dateElement.value == null || dateElement.value == undefined) {
            return;
        }

        let createArticle = document.createElement('article');
        let createDescriptionParagraph = document.createElement('p');
        let createDateParagraph = document.createElement('p');
        let createDivForBtns = document.createElement('div');
        let createStartBtn = document.createElement('button');
        let createDeleteBtn = document.createElement('button');
        let createTaskHeading = document.createElement('h3');

        // Add classes and textContent to btns

        createDivForBtns.classList.add("flex");
        createStartBtn.classList.add("green");
        createStartBtn.textContent = 'Start';
        createDeleteBtn.classList.add("red");
        createDeleteBtn.textContent = 'Delete';

        //

        createDivForBtns.appendChild(createStartBtn);
        createDivForBtns.appendChild(createDeleteBtn);

        createTaskHeading.textContent = `${taskElement.value}`;
        createDescriptionParagraph.textContent = `Description: ${descriptionElement.value}`;
        createDateParagraph.textContent = `Due Date: ${dateElement.value}`;

        createArticle.appendChild(createTaskHeading);
        createArticle.appendChild(createDescriptionParagraph);
        createArticle.appendChild(createDateParagraph);
        createArticle.appendChild(createDivForBtns);
        sectionOpen.appendChild(createArticle);

        createDeleteBtn.addEventListener('click', (e) => {
            sectionOpen.removeChild(createArticle);
        });

        createStartBtn.addEventListener('click', (e) => {

            sectionInProgress.appendChild(createArticle);
            let delBtn = createArticle.querySelector('button:nth-child(1)');
            delBtn.textContent = 'Delete';
            delBtn.classList.remove("green");
            delBtn.classList.add("red");

            let finishBtn = createArticle.querySelector('button:nth-child(2)');
            finishBtn.textContent = 'Finish';
            finishBtn.classList.remove("red");
            finishBtn.classList.add("orange");

            // sectionOpen.removeChild(createArticle);

            delBtn.addEventListener('click', (e) => {

                sectionInProgress.removeChild(e.target.parentElement.parentElement);
            })

            finishBtn.addEventListener('click', () => {
                let divWithBtns = createArticle.querySelector('.flex');
                createArticle.removeChild(divWithBtns);
                sectionComplete.appendChild(createArticle);
                return;
            })
        });
    }
}