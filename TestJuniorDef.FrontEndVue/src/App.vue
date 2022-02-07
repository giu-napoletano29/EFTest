<template>
    <div id="app">
        <div class="base-layout">
            <b-navbar type="dark" variant="dark" class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
                <b-navbar-brand href="#" class="navbar-brand col-md-3 col-lg-2 me-0 px-3">Company name</b-navbar-brand>
            </b-navbar>
        </div>
        <div class="container-fluid">
            <div class="row flex-xl-nowrap2">
                <nav id="bd-docs-nav" aria-label="Main navigation" class="base-layout col-md-3 col-lg-2 d-md-block bg-light sidebar collapse">
                    <div class="position-sticky pt-3">
                        <ul class="nav flex-column">
                            <li class="nav-item">
                                <router-link to="/" class="nav-link active">Home</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link to="/about" class="nav-link">About</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link to="/brands" class="nav-link">Brand</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link to="/products" class="nav-link">Prodotti</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link to="/leeds" class="nav-link">Richieste info</router-link>
                            </li>
                        </ul>
                    </div>
                </nav>
                <div class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
                    <router-view />
                </div>
            </div>
        </div>
    </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  /* text-align: center; */
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
