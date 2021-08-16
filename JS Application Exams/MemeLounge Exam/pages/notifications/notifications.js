import { notificationsTemplate } from "./notificationsTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _notifications = [];
let _id = 0;

function initialize(router, renderHandler) {
    _router = router;
    _renderHandler = renderHandler;
}

async function createNotification(message) {
    let newNotification = {
        id: _id++,
        message
    };

    _notifications.push(newNotification);

    let templateResult = notificationsTemplate(_notifications);
    _renderHandler(templateResult);

    setTimeout(() => {
        _notifications = _notifications.filter(n => n.id !== newNotification.id);
        let templateResult = notificationsTemplate(_notifications);
        _renderHandler(templateResult);
    } ,5000);
}

export default {
    createNotification,
    initialize
}