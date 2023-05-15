<template>
<div>
  <HeaderBarComponent></HeaderBarComponent>
  <main>
    <RouterView></RouterView>
  </main>
  <footer>

  </footer>

</div>
</template>

<script>

import HeaderBarComponent from './components/HeaderBarComponent.vue';

export default {
  components: {
    HeaderBarComponent
  }
}
</script>
