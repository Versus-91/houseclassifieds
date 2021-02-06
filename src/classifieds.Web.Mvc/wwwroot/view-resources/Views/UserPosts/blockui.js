Vue.component("app-block-ui", {
    props: ['message', 'url', 'html'],
    template: `<div class="loading-container">
        <div class="loading-backdrop"></div>
        <div class="loading">
            <div class="loading-icon">
                <img class="image is-96x96 mr-5" v-if="url" :src="url"/>
                <div v-if="!url && html" v-html="html"></div>
                <slot></slot>
            </div>
            <div v-if="message" class="loading-label">{{message}}</div>
        </div>
    </div>`
});
