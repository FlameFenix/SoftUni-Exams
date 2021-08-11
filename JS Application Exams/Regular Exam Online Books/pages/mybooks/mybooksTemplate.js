import { html } from "./../../node_modules/lit-html/lit-html.js";

export let mybooksTemplate = (model) => html`
        <section id="my-books-page" class="my-books">
            <h1>My Books</h1>
            ${model.books.length > 0 
            ? html`
            <ul class="my-books-list">
            ${model.books.map(x => bookModel(x))}
            </ul>
            `
            : html `<p class="no-books">No books in database!</p>`}    
        </section>
`;

let bookModel = (model) => html`
<li class="otherBooks">
                    <h3>${model.title}</h3>
                    <p>Type: ${model.type}</p>
                    <p class="img"><img src="${model.imageUrl}"></p>
                    <a class="button" href="/details/${model._id}">Details</a>
                </li>
`;