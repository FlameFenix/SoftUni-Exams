import { html } from "./../../node_modules/lit-html/lit-html.js";

export let allMemesTemplate = (model) => html`
<section id="meme-feed">
            <h1>All Memes</h1>
            <div id="memes">
                ${model.length > 0 ? html`${model.map(x => memeModel(x))}`
                : html`<p class="no-memes">No memes in database.</p>`}
			</div>
        </section>
`;

let memeModel = (model) => html`
<div class="meme">
                    <div class="card">
                        <div class="info">
                            <p class="meme-title">${model.title}</p>
                            <img class="meme-image" alt="meme-img" src="${model.imageUrl}">
                        </div>
                        <div id="data-buttons">
                            <a class="button" href="/details/${model._id}">Details</a>
                        </div>
                    </div>
                </div>
`