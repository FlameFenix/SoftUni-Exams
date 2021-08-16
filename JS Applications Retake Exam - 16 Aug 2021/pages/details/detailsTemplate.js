import { html } from "./../../node_modules/lit-html/lit-html.js";

export let detailsTemplate = (details) => html`
<section id="game-details">
<h1>Game Details</h1>
<div class="info-section">

    <div class="game-header">
        <img class="game-img" src="${details.game.imageUrl}" />
        <h1>${details.game.title}</h1>
        <span class="levels">MaxLevel: ${details.game.maxLevel}</span>
        <p class="type">${details.game.category}</p>
    </div>

    <p class="text">
    ${details.game.summary}
    </p>

    <!-- Bonus ( for Guests and Users ) -->
    <div class="details-comments">
        <h2>Comments:</h2>
        <ul>
            <!-- list all comments for current game (If any) -->
            <li class="comment">
                <p>Content: I rate this one quite highly.</p>
            </li>
            <li class="comment">
                <p>Content: The best game.</p>
            </li>
        </ul>
        <!-- Display paragraph: If there are no games in the database -->
        <p class="no-comment">No comments.</p>
    </div>
    ${details.isOwner ? html`<div class="buttons">
    <a href="/edit/${details.game._id}" class="button">Edit</a>
    <a href="javascript:void(0)" @click=${(e) => details.deleteHandler(details.game._id, e)} class="button">Delete</a>`: ''}
</div>
    
</div>

<!-- Bonus -->
<!-- Add Comment ( Only for logged-in users, which is not creators of the current game ) -->
<article class="create-comment">
    <label>Add new comment:</label>
    <form class="form">
        <textarea name="comment" placeholder="Comment......"></textarea>
        <input class="btn submit" type="submit" value="Add Comment">
    </form>
</article>

</section>
`;