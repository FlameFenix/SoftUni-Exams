import { html } from "./../../node_modules/lit-html/lit-html.js";

export let detailsTemplate = (model) => html`

${model.isOwner 
     ? html`
     <section id="listing-details">
<h1>Details</h1>
            <div class="details-info">
                <img src=${model.listing.imageUrl}>
                <hr>
                <ul class="listing-props">
                    <li><span>Brand:</span>${model.listing.brand}</li>
                    <li><span>Model:</span>${model.listing.model}</li>
                    <li><span>Year:</span>${model.listing.year}</li>
                    <li><span>Price:</span>${model.listing.price}$</li>
                </ul>
                <p class="description-para">${model.listing.description}</p>
                
                <div class="listings-buttons">
                <a href="/edit/${model.listing._id}" class="button-list">Edit</a>
                <a href="/listings" class="button-list" @click=${(e) => model.deleteHandler(model.listing._id, e)}>Delete</a>
                </div>
            </div>
            </section>`
     : html`
     <section id="listing-details">
<h1>Details</h1>
            <div class="details-info">
                <img src=${model.listing.imageUrl}>
                <hr>
                <ul class="listing-props">
                    <li><span>Brand:</span>${model.listing.brand}</li>
                    <li><span>Model:</span>${model.listing.model}</li>
                    <li><span>Year:</span>${model.listing.year}</li>
                    <li><span>Price:</span>${model.listing.price}$</li>
                </ul>
                <p class="description-para">${model.listing.description}</p>
            </div>
            </section>
     ` }

`;

let ownersButtons = () => html`

`;

let notOwners = () => html`
<section>
<h1>Details</h1>
            <div class="details-info">
                <img src=${model.listing.imageUrl}>
                <hr>
                <ul class="listing-props">
                    <li><span>Brand:</span>${model.listing.brand}</li>
                    <li><span>Model:</span>${model.listing.model}</li>
                    <li><span>Year:</span>${model.listing.year}</li>
                    <li><span>Price:</span>${model.listing.price}$</li>
                </ul>
                <p class="description-para">${model.listing.description}</p>
            </div>
            </section>
`;
