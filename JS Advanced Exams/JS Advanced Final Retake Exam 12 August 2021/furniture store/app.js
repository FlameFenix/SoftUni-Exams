window.addEventListener('load', solve);

function solve() {

    let form = document.querySelector('form');
    let addBtn = document.querySelector('form button');

    addBtn.addEventListener('click', (e) => {
        e.preventDefault();
        let model = document.getElementById('model').value;
        let year = document.getElementById('year').value;
        let description = document.getElementById('description').value;
        let price = document.getElementById('price').value;

        if (model == '' || year == '' || description == '' || price == '' || year < 0 || price < 0) {
            return;
        }

        form.reset();

        let tableBody = document.getElementById('furniture-list');

        let tableRowInfo = document.createElement('tr');
        tableRowInfo.classList.add("info");

        let tdModel = document.createElement('td');
        tdModel.textContent = model;

        let tdPrice = document.createElement('td');
        tdPrice.textContent = Number(price).toFixed(2);

        let tdBtns = document.createElement('td');

        let btnMoreInfo = document.createElement('button');
        btnMoreInfo.classList.add("moreBtn");
        btnMoreInfo.textContent = 'More Info';

        let btnbuyBtn = document.createElement('button');
        btnbuyBtn.classList.add("buyBtn");
        btnbuyBtn.textContent = 'Buy it';
       
        let trMoreInfo = document.createElement('tr');
        trMoreInfo.classList.add("hide");

        let tdYear = document.createElement('td');
        tdYear.textContent = `Year: ${Number(year)}`;

        let tdDescription = document.createElement('td');
        tdDescription.setAttribute("colspan", "3");
        tdDescription.textContent = `Description: ${description}`;

        tdBtns.appendChild(btnMoreInfo);
        tdBtns.appendChild(btnbuyBtn);
        tableRowInfo.appendChild(tdModel);
        tableRowInfo.appendChild(tdPrice);
        tableRowInfo.appendChild(tdBtns);

        trMoreInfo.appendChild(tdYear);
        trMoreInfo.appendChild(tdDescription);

        tableBody.appendChild(tableRowInfo);
        tableBody.appendChild(trMoreInfo);

        btnMoreInfo.addEventListener('click', (e) => {
            e.preventDefault();
            if (e.currentTarget.textContent == 'More Info') {
                e.currentTarget.textContent = 'Less Info';
                trMoreInfo.style.display = 'contents'
            } else {
                e.currentTarget.textContent = 'More Info';
                trMoreInfo.style.display = 'none'
            }
        })

        btnbuyBtn.addEventListener('click', (e) => {
            e.preventDefault();

            let element = e.currentTarget.parentElement.parentElement.parentElement.querySelector('.hide');
            element.remove();
            e.target.parentElement.parentElement.remove();

            let totalPrice = document.querySelector('.total-price');
            let currentPrice = 0;

            let currentTotalPrice = Number(totalPrice.textContent).toFixed(2);
            let productPrice = Number(tdPrice.textContent).toFixed(2);

            currentPrice = Number(currentTotalPrice) + Number(productPrice);
            totalPrice.textContent = currentPrice.toFixed(2);

            
        })
    })
}
