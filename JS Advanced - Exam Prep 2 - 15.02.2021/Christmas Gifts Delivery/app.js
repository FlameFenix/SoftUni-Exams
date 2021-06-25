function solution() {
    // Elements
    let buttonElement = document.querySelector('button');
    let giftNameInput = document.querySelector('input');
    let listOfGifts = document.querySelector('.card:nth-child(2) ul');
    let listOfSendGifts = document.querySelector('.card:nth-child(3) ul');
    let listOfDiscardGifts = document.querySelector('.card:nth-child(4) ul');


    console.log(listOfGifts);
    // Events
    buttonElement.addEventListener('click', addGiftToList)
    // New elements

    let listOfAddedGifts = [];

    function addGiftToList(event) {

        let listItem = document.createElement('li');
        listItem.classList.add("gift");
        listItem.textContent = giftNameInput.value;
        listOfAddedGifts.push(listItem);

        let sendButton = document.createElement('button');
        sendButton.setAttribute("id", "sendButton")
        sendButton.textContent = 'Send';
        let discardButton = document.createElement('button');
        discardButton.textContent = 'Discard'
        discardButton.setAttribute("id", "discardButton");

        listItem.appendChild(sendButton);
        listItem.appendChild(discardButton);
        sendButton.addEventListener('click', sendGift)
        discardButton.addEventListener('click', discardGift);

        listOfAddedGifts.sort((a, b) => a.textContent.localeCompare(b.textContent)).forEach(el => {
            listOfGifts.appendChild(el);
        });

        console.log(listItem);

        giftNameInput.value = '';
        

        function sendGift(e) {
            let liItem = e.target.parentElement;
            let index = listOfAddedGifts.indexOf(liItem);
            listOfAddedGifts.splice(index, 1);
            listOfGifts.removeChild(e.target.parentElement);
            let sendItem = document.createElement('li');
            sendItem.textContent = liItem.textContent.replace('Send', '').replace('Discard', '');
            listOfSendGifts.appendChild(sendItem);
        }

        function discardGift(e) {
            let liItem = e.target.parentElement;
            let index = listOfAddedGifts.indexOf(liItem);
            listOfAddedGifts.splice(index, 1);
            listOfGifts.removeChild(e.target.parentElement);
            let discardItem = document.createElement('li');
            discardItem.textContent = liItem.textContent.replace('Send', '').replace('Discard', '');
            listOfDiscardGifts.appendChild(discardItem);
        }
    }


}