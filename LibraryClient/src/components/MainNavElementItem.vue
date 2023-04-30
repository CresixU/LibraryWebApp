<template>
    <div>
        <i :class="classProp"></i>
        <span><slot></slot></span>
    </div>
</template>
<script>
export default {
    props: {
        classProp: String
    }
}
</script>