<template>
    <div class="lightCard">
        <h1>Add Series</h1>
        <input type="text" name="seriesname" v-model="input.seriesname" placeholder="seriesname" />
        <button type="button" class="btn" v-on:click="addSeries()">Add Series</button>
    </div>
</template>

<script>
    export default {
        name: "Series",
        data() {
            return {
                input: {
                    seriesname: ""
                }
            }
        },
        methods: {
            addSeries() {
                if (this.input.seriesname != "" && this.input.seriesname != null ) {

                    const requestOptions = {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' }
                    };
                    fetch('EpisodesWatched/AddSeriesWatched?name=' + this.input.seriesname, requestOptions)
                        .then(async response => {
                            const data = await response.text();

                            if (response.ok) {

                                this.$router.replace({ name: "Watched" });

                            }

                        })
                        .catch(error => {
                            this.errorMessage = error;
                            console.error('Error!', error);
                        });

                } else {
                    console.log("Enter Series Name");
                }
            }
        }
    }
</script>

<style scoped></style>