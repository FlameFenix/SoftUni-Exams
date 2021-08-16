import { html } from "./../../node_modules/lit-html/lit-html.js";

export let navigationTemplate = (model) => html`
<h1><a class="home" href="/home">GamesPlay</a></h1>
            <nav>
                <a href="/catalogue">All games</a>
                <!-- Logged-in users -->
                <div id="user">
                    <a href="/create">Create Game</a>
                    <a href="javascript:void(0)" @click=${model.logoutHandler}>Logout</a>
                </div>
                <!-- Guest users -->
                <div id="guest">
                    <a href="/login">Login</a>
                    <a href="/register">Register</a>
                </div>
            </nav>
`;