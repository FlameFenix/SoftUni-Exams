import { html } from "./../../node_modules/lit-html/lit-html.js";

export let editTemplate = (model) => html`
<section id="edit-page" class="auth">
            <form @submit=${(e) => model.submitHandler(model.game._id, e)}id="edit">
                <div class="container">

                    <h1>Edit Game</h1>
                    <label for="leg-title">Legendary title:</label>
                    <input type="text" id="title" name="title" value="${model.game.title}">

                    <label for="category">Category:</label>
                    <input type="text" id="category" name="category" value="${model.game.category}">

                    <label for="levels">MaxLevel:</label>
                    <input type="number" id="maxLevel" name="maxLevel" min="1" value="${model.game.maxLevel}">

                    <label for="game-img">Image:</label>
                    <input type="text" id="imageUrl" name="imageUrl" value="${model.game.imageUrl}">

                    <label for="summary">Summary:</label>
                    <textarea name="summary" id="summary">${model.game.summary}}</textarea>
                    <input class="btn submit" type="submit" value="Edit Game">

                </div>
            </form>
        </section>
`;