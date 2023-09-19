<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Loading...
        </div>

        <div v-if="post" class="content lightCard">
            <h1>{{name}}</h1>
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Season</th>
                        <th>Episode</th>
                        <th>_</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="episodes in post" :key="episodes.id">
                        <td>{{ episodes.name }}</td>
                        <td>{{ episodes.season }}</td>
                        <td>{{ episodes.number }}</td>
                        <td v-if="isWatched(this.name + episodes.season + episodes.number)" style="background-color: forestgreen;">Watched</td>
                        <td v-else><button type="button" class="btn" v-on:click="addEpisode(this.name, episodes.season, episodes.number)">Add To Watched</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                post: null,
                name: null,
                watchList: [],
                sName: null
            };
        },
        props: ['id'],
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
            
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;
                this.name = null;
                this.watchList = [];

                var searchName = this.id;


                fetch('EpisodesWatched/GetEpisodesWatched?name=' + searchName)
                    .then(async response => {
                        const data = await response.json();
                        if (response.ok) {
                            Object.values(data).forEach((value) => {
                                this.watchList.push(value.seriesName + value.season + value.episode);
                            });
                        }
                    });

                fetch('https://api.tvmaze.com/singlesearch/shows?q=' + searchName + '&embed=episodes')
                    .then(async response => {
                        const data = await response.json();
                        const ep = data._embedded.episodes;

                        this.name = data.name;
                        this.post = ep;
                        this.loading = false;
                        return;
                    });
            },
            addEpisode(name, season, episode) {

                var date = new Date();
                const requestOptions = {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ id: 0, username: '', seriesName: name, season: season.toString(), episode: episode.toString(), insertdate: date })
                };
                fetch('EpisodesWatched/AddEpisodesWatched', requestOptions)
                    .then(async response => {
                        const data = await response.text();

                        if (!response.ok) {

                            console.error('Save Failed');

                        } else if (data != "false") {

                            this.$router.replace({ name: "Watched" });

                        } else {

                            console.error('Save Failed');

                        }


                    })
                    .catch(error => {
                        this.errorMessage = error;
                        console.error('Error!', error);
                    });
            },
            isWatched(name) {
                return this.watchList.includes(name)
            }
        },
    });
</script>