<template>
    <div class="page-wrap d-flex flex-row align-items-center">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-12 text-center">
                    <span class="display-1 d-block">404</span>
                    <div class="mb-4 lead">La pagina che stavi cercando non è stata trovata.</div>
                    <router-link to="/" class="nav-link">Torna alla Home</router-link>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
body {
    background: #dedede;
}
.page-wrap {
    min-height: 80vh;
}
</style>