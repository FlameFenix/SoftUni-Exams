import { html } from "./../../node_modules/lit-html/lit-html.js";

export let navigationTemplate = (model) => html`
    <section class="navbar-dashboard">
        <a href="/dashboard">Dashboard</a>
        ${model.isLoggedIn
        ? loggedUsers(model)
        : guestsUsers() }
    </section>
`;

let loggedUsers = (model) => html`
<div id="user">
            <span>Welcome, ${model.email}</span>
            <a class="button" href="/mybooks">My Books</a>
            <a class="button" href="/create">Add Book</a>
            <a class="button" href="javascript:void(0)" @click=${model.logoutHandler}>Logout</a>
        </div>
`

let guestsUsers = () => html`
<div id="guest">
            <a class="button" href="/login">Login</a>
            <a class="button" href="/register">Register</a>
        </div>
`