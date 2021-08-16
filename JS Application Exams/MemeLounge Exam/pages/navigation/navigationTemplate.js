import { html } from "./../../node_modules/lit-html/lit-html.js";

export let navigationTemplate = (navModel) => html`
    
    ${navModel.isLoggedIn
        ? html`
    <a href="/all-memes">All Memes</a>
    <div class="user">
    <a href="/create">Create Meme</a>
     <div class="profile">
           <span>Welcome, ${navModel.email}</span>
            <a href="/profile">My Profile</a>
            <a href="javascript:void(0)" @click=${(e) => navModel.logoutHandler(e)}>Logout</a>
        </div>
    </div>
    `
        : html`
        
    <div class="guest">
    <div class="profile">
        <a href="/login">Login</a>
        <a href="/register">Register</a>
    </div>
    <a class="active" href="/home">Home Page</a>
    <a href="/all-memes">All Memes</a>
    </div>
    ` }
    `;