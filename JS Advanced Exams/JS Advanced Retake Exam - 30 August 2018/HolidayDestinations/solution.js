function addDestination() {
    let cityInput = document.querySelector('.inputData:nth-child(2)');
    let countryInput = document.querySelector('.inputData:nth-child(4)');
    let seasonInput = document.querySelector('#seasons');
    let tbody = document.getElementById('destinationsList');

    let createTableRow = document.createElement('tr');
    let destinationElement = document.createElement('td');
    let seasonElement = document.createElement('td');

    if (cityInput.value == '' || cityInput.value == null || cityInput.value == undefined) {
        return;
    }

    if (countryInput.value == '' || countryInput.value == null || countryInput.value == undefined) {
        return;
    }

    destinationElement.textContent = `${cityInput.value}, ${countryInput.value}`;

    let optionElement = Array.from(document.querySelectorAll('option'));

    for (const el of optionElement) {
        if (el.value == seasonInput.value) {
            seasonElement.textContent = el.textContent;
        }
    }

    cityInput.value = '';
    countryInput.value = '';

    createTableRow.appendChild(destinationElement);
    createTableRow.appendChild(seasonElement);

    tbody.appendChild(createTableRow);

    let counter = document.querySelectorAll('.summary');

    for (const item of counter) {
        let attribute = item.getAttribute("id");
        console.log(attribute);
        if (attribute.toLowerCase() == seasonElement.textContent.toLowerCase()) {
            item.value++;
        }
    }
}