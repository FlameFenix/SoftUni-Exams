function solve() {
    let lectureName = document.querySelector('.form-control:nth-child(1) input');
    let lectureDate = document.querySelector('.form-control:nth-child(2) input');
    let lectureModule = document.querySelector('.form-control:nth-child(3) select');
    let addButton = document.querySelector('.form-control:nth-child(4) button');
    let trainingSection = document.querySelector('.modules');

    addButton.addEventListener('click', (e) => {
        e.preventDefault();

        if (lectureName.value === undefined || lectureName.value === '' ||
            lectureDate.value == null || lectureModule.value == 'Select module') {
            return;
        }

        let h3Element = document.createElement('h3');
        h3Element.textContent = lectureModule.value;
        h3Element.setAttribute("style", "background-color: #FFA000;");

        let ulElement = document.createElement('ul');
        let liElement = document.createElement('li');
        liElement.classList.add("flex");

        let h4Element = document.createElement('h4');
        let splittedDate = lectureDate.value.split('T');
        let date = splittedDate[0];
        let hours = splittedDate[1];

        h4Element.textContent = `${lectureName.value} - ${date.replaceAll('-', '/')} ${hours}`;

        let delBtnElement = document.createElement('button');
        delBtnElement.classList.add("red");
        delBtnElement.textContent = 'Del';

        liElement.appendChild(h4Element);
        liElement.appendChild(delBtnElement);
        ulElement.appendChild(liElement);

        let isExist = false;

        for (const child of trainingSection.children) {
            if (child.textContent == h3Element.textContent) {
                trainingSection.querySelector('ul').appendChild(liElement);
                isExist = true;
            }
        }

        if (!isExist) {
            trainingSection.appendChild(h3Element)
            trainingSection.appendChild(ulElement);
        }

        let sorted = [];
        let dates = [];

        for (const heading of trainingSection.querySelectorAll('h4')) {
            let date = heading.textContent.split(' - ');
            dates.push(date[1]);
        }

        for (const date of dates.sort((a,b) => a.localeCompare(b))) {
            trainingSection.querySelectorAll('h4').forEach((x) => x.textContent.includes(date), () => {
                sorted.push(x);
            })
        }

        console.log(sorted);

        delBtnElement.addEventListener('click', (e) => {
            let ultargeting = trainingSection.querySelector('ul')
            ultargeting.removeChild(e.target.parentElement);
            if (ultargeting.children.length == 0) {
                trainingSection.parentElement.removeChild(trainingSection);
            }
        });
    })

};