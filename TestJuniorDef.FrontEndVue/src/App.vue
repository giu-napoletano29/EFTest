<template>
    <div id="app">
        <div>
            <b-navbar type="dark" variant="dark" class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
                <b-navbar-brand href="#" class="navbar-brand col-md-3 col-lg-2 me-0 px-3">Company name</b-navbar-brand>
            </b-navbar>
        </div>
        <div class="container-fluid">
            <div class="row flex-xl-nowrap2">
                <!--<div id="nav">
                <router-link to="/">Home</router-link> |
                <router-link to="/about">About</router-link>
            </div>-->
                <nav id="bd-docs-nav" aria-label="Main navigation" class="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse">
                    <div class="position-sticky pt-3">
                        <h2><router-link to="/">Home</router-link></h2>
                        <h2><router-link to="/about">About</router-link></h2>
                        <h2>test</h2>
                        <h2>test</h2>
                        <h2>test</h2>
                        <h2>test</h2>
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
  text-align: center;
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
