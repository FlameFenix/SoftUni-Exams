import { html } from "./../../node_modules/lit-html/lit-html.js";

export let editTemplate = (edit) => html`
<section id="edit-listing">
            <div class="container">

                <form @submit=${(e) => edit.submitHandler(edit.listing._id, e)} id="edit-form">
                    <h1>Edit Car Listing</h1>
                    <p>Please fill in this form to edit an listing.</p>
                    <hr>

                    <p>Car Brand</p>
                    <input type="text" placeholder="Enter Car Brand" name="brand" value="${edit.listing.brand}">

                    <p>Car Model</p>
                    <input type="text" placeholder="Enter Car Model" name="model" value="${edit.listing.model}">

                    <p>Description</p>
                    <input type="text" placeholder="Enter Description" name="description" value="${edit.listing.description}">

                    <p>Car Year</p>
                    <input type="number" placeholder="Enter Car Year" name="year" value="${Number(edit.listing.year)}">

                    <p>Car Image</p>
                    <input type="text" placeholder="Enter Car Image" name="imageUrl" value="${edit.listing.imageUrl}">

                    <p>Car Price</p>
                    <input type="number" placeholder="Enter Car Price" name="price" value="${Number(edit.listing.price)}">

                    <hr>
                    <input type="submit" class="registerbtn" value="Edit Listing">
                </form>
            </div>
        </section>
`;