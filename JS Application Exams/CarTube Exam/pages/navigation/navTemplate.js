import { html } from "./../../node_modules/lit-html/lit-html.js";

export let navTemplate = (nav) => html`
<a class="active" href="/home">Home</a>
<a href="/listings">All Listings</a>
<a href="#">By Year</a>
${nav.isLoggedIn ? 
    loggedUsers(nav) 
    : guestUsers()}
`;
let guestUsers = () => html`
<div id="guest">
    <a href="/login">Login</a>
    <a href="/register">Register</a>
</div>
`;
let loggedUsers = (nav) => html`
<div id="profile">
    <a>Welcome ${nav.email}</a>
    <a href="/mylistings">My Listings</a>
    <a href="/create">Create Listing</a>
    <a href="/home" @click=${nav.logoutHandler}>Logout</a>
</div>
`;

