import { createTemplate } from "./createTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _memesService = undefined;
let _form = undefined;

function initialize(router, renderHandler, memesService) {
    _router = router;
    _renderHandler = renderHandler;
    _memesService = memesService;
}

async function submitHandler(e){
    e.preventDefault();
    try{
        let formData = new FormData(e.target);
        let emptyFields = false;
        let title = formData.get('title');
        if(title.trim() === ''){
            window.alert('Title is required');
            emptyFields = true;
        }

        let description = formData.get('description');
        if(description.trim() === ''){
            window.alert('Description is required');
            emptyFields = true;
        }

        let imageUrl = formData.get('imageUrl');
        if(imageUrl.trim() === ''){
            window.alert('Image Url is required');
            emptyFields = true;
        }

        if(emptyFields) {
            return;
        }
        
        let meme = {
            title,
            description,
            imageUrl
        }
    
        let loginResult = await _memesService.create(meme);
        _router.redirect('/allmemes');
    } catch (err){
        alert(err);
    }
   
}

async function getView(context) {
    _form = {
        submitHandler,
    }
    let templateResult = createTemplate(_form);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}