import {html} from "./../../node_modules/lit-html/lit-html.js";

export let editTemplate = (model) => html`
<section @submit=${(e) => model.submitHandler(model.meme._id, e)} id="edit-meme">
            <form id="edit-form">
                <h1>Edit Meme</h1>
                <div class="container">
                    <label for="title">Title</label>
                    <input id="title" type="text" placeholder="${model.meme.title}" name="title">
                    <label for="description">Description</label>
                    <textarea id="description" placeholder="Enter Description" name="description">${model.meme.title}</textarea>
                    <label for="imageUrl">Image Url</label>
                    <input id="imageUrl" type="text" placeholder="${model.meme.imageUrl}" name="imageUrl">
                    <input type="submit" class="registerbtn button" value="Edit Meme">
                </div>
            </form>
        </section>
`;