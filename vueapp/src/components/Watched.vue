<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Loading...
        </div>



        <div class="lightCard">
            <h1>Add New Series</h1>
            <router-link to="/Series"><button type="button" class="btn">Add Series</button></router-link>

        </div>

        <div>
            

        </div>

        <div v-if="post" class="content lightCard">
            <h1>Currently Tracked Shows</h1>
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="series in post" :key="series.seriesName">
                        <td>{{ series.seriesName }}</td>
                        <td><button type="button" class="btn" v-on:click="passSeriesName(series.seriesName)">Episodes</button></td>
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
                post: null
            };
        },
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

                fetch('EpisodesWatched/GetSeriesWatched')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });
            },
            passSeriesName(sname) {
                this.$router.push({ name: "Episodes", params: { id: sname } })
            }

        },
    });
</script>