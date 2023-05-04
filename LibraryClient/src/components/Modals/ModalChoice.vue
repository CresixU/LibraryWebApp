<template>
<div class="modal fade" :id="modalId" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">{{ title }}</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <p><slot>Default text</slot></p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">{{ btnNo }}</button>
                <button type="button" class="btn btn-primary" data-bs-dismiss="modal" @click="CallParentTrueAction()">{{ btnYes }}</button>
            </div>
        </div>
    </div>
</div>
</template>
<script>
export default {
    props: {
        modalId: {
            type: String,
            required: true
        },
        title: {
            type: String, 
            default: 'No title'
        },
        btnYes: {
            type: String,
            default: 'Save changes'
        },
        btnNo: {
            type: String,
            default: 'Close'
        },
        trueData: {}
    },
    methods: {
        CallParentTrueAction() {
            this.$emit('CallParentTrueAction', this.trueData)
        }
    }
}
</script>