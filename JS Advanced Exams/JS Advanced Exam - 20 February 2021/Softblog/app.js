function solve() {
   let postElement = document.querySelector('main section');

   // input
   let authorElement = document.querySelector('#creator');
   let titleElement = document.getElementById('title');
   let categoryElement = document.getElementById('category');
   let contentElement = document.getElementById('content');
   let archiveSection = document.querySelector('.archive-section ol');

   // EventListener
   let buttonElement = document.querySelector('button');
   buttonElement.addEventListener('click', createElement);

   function createElement(event) {
      // Tree
      let articleElement = document.createElement('article');
      let h1Element = document.createElement('h1');
      let pCategoryElement = document.createElement('p');
      let strongElement = document.createElement('strong');
      let pAuthorElement = document.createElement('p');
      let authorStrongElement = document.createElement('strong');
      let pContentElement = document.createElement('p');
      let divElement = document.createElement('div');
      let deleteBtn = document.createElement('button');
      let archiveBtn = document.createElement('button');
      let archiveTitle = document.createElement('li');

      event.preventDefault();
      postElement.appendChild(articleElement);
      h1Element.textContent = titleElement.value;
      articleElement.appendChild(h1Element);
      strongElement.textContent = categoryElement.value;
      pCategoryElement.textContent = 'Category:';
      pCategoryElement.appendChild(strongElement);
      articleElement.appendChild(pCategoryElement);
      authorStrongElement.textContent = authorElement.value;
      pAuthorElement.textContent = 'Creator:'
      pAuthorElement.appendChild(authorStrongElement);
      articleElement.appendChild(pAuthorElement);
      pContentElement.textContent = contentElement.value;
      articleElement.appendChild(pContentElement);
      deleteBtn.textContent = 'Delete';
      archiveBtn.textContent = 'Archive';
      deleteBtn.classList.add("btn", "delete");
      archiveBtn.classList.add("btn", "archive");
      divElement.classList.add('buttons');
      divElement.appendChild(deleteBtn);
      divElement.appendChild(archiveBtn);
      articleElement.appendChild(divElement);

      archiveBtn.addEventListener('click', archiveArticle);
      deleteBtn.addEventListener('click', deleteArticle);

      function deleteArticle(event) {
         postElement.removeChild(event.target.parentElement.parentElement);
      }

      function archiveArticle(event) {
         if (h1Element.textContent != '') {
            postElement.removeChild(event.target.parentElement.parentElement);
            archiveTitle.textContent = h1Element.textContent;   
            let orderedLi = Array.from(document.querySelectorAll('li'));
            orderedLi.push(archiveTitle);
            orderedLi = orderedLi.sort((a, b) => a.textContent.localeCompare(b.textContent))
                                 .forEach(x => archiveSection.appendChild(x));
         }
      }

      authorElement.value = '';
      titleElement.value = '';
      categoryElement.value = '';
      contentElement.value = '';
   }
}
