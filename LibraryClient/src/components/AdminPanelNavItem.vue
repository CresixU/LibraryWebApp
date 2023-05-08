<template>
<div class="box">
    <div>
        <slot></slot>
    </div>
</div>

</template>

<style scoped>
.box {
    background-color: white;
    color:#351672;
    padding: 10px 15px;
    margin: 15px auto;
    font-weight: 700;
    display: flex;
    border-left: 5px solid #712cf9;
    cursor: pointer;
    transition: all 0.5s;
}
.box:hover {
    background-color: #712cf9;
    border-left: 5px solid #3f0da5;
    color: white;
}
</style>