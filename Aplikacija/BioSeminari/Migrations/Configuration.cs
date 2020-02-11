namespace MSDNRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Seminari.AddOrUpdate(
                new Models.Seminar
                {
                    Id = 1,
                    Naziv = "Biostatistika",
                    Opis = "Statistika - grana primijenjene matematike koja se bavi analizom podataka.\nBiostatistika je primjenjena statistika na području biomedicinskih znanosti. Studenti će upoznati najčešće razdiobe u bio‐znanostima, razumjeti osnove teorije uzoraka, objasniti stohastičku vezu i matematičko modeliranje, znati primijeniti statističke testove te razumjeti njihova rješenja, znati osmisliti te provesti statističku obradu bioloških pokusa.",
                    Datum = new DateTime(2018, 1, 22, 8, 0, 0),
                    Popunjen = false
                },

                 new Models.Seminar
                 {
                     Id = 2,
                     Naziv = "Mikologija",
                     Opis = "Mikologija je grana biologije koja izučava gljive, uključjući i njihova genetička i biohemijska svojstva, taksonomiju, upotrebu kod ljudi kao lijekove (penicilin), hranu (pivo, vino, sir, jestive gljive) i enteogense, kao i opasnosti koje dolaze od njih, poput trovanja ili infekcije. Mikologija je multidisciplinarna, jer proučava građu, metabolizam, biohemiju, ekologiju, evoluciju i sistematiku gljiva, oslaljajući se na metode i rezultate i drugih bioloških disciplina.\nOd mikologije je nastala fitopatologija, nauka o bolestima biljaka, i ove dvije grane su i dalje blisko povezane zato što su mnoge gljive biljni patogeni.Gljive su osnova života na Zemlji u svojoj ulozi simbionta (lišajevi su zapravo simbioza između mahovina i gljiva). Važne su i jer razlažu kompleksne organske biomolekule poput lignina, izdržljive komponente drveta. Igraju i važnu ulogu u globalnom ciklusu ugljika. Razne gljive i drugi organizmi koji se često svrstavaju u gljive su često ekonomski i sociološki važne, jer neke izazivaju bolesti životinja kao i biljaka. Nauka o patogenim gljivama naziva se medicinska mikologija.",
                     Datum = new DateTime(2018, 3, 27, 16, 30, 0),
                     Popunjen = false
                 },

                 new Models.Seminar
                 {
                     Id = 3,
                     Naziv = "Biogeografija",
                     Opis = "Biogeografija je znanost o rasprostranjenosti vrsta obično promatranih od regionalnih do kontinentskih razmjera. Primjer rasprostranjenosti na toj razini može se objasniti kombinacijom povijesnih faktora povezanih s prostorom i izolacijom kopna, povezanih s dostupnom zalihom energije. Klasična biogeografija je dobila uzlet razvitkom molekularne sistematike. Taj je razvoj omogućio znanstvenicima da testiraju teorije o podrijetlu i disperziji populacija (npr. otočnih endema). Dok je primjerice klasična biogeografija mogla nagađati o podrijetlu vrsta na Havajskom otočju, molekularna sistematika je omogućila znanstvenicima da testiraju teorije o srodstvu tih populacija s pretpostavljenim izvorom populacija u Aziji i Sjevernoj Americi. Biogeografija je sintetička znanost usko povezana s geografijom, biologijom, geologijom, klimatologijom i ekologijom.",
                     Datum = new DateTime(2018, 2, 28, 12, 0, 0),
                     Popunjen = false
                 },

                new Models.Seminar
                {
                    Id = 4,
                    Naziv = "Imunologija",
                    Opis = "Prijavite se za BESPLATNE jednodnevne osmosatne seminare po čijem završetku isti dan polažete certifikacijski ispit. Imati Google certifikat znači nametnuti se svojim klijentima kao profesionalac koji je uspješno savladao napredne funkcije Googleove platforme AdWords.\nVaši klijenti vrednovat će Vas prema rezultatima konverzija koje za njih osiguravate, a upravo ovdje stečena znanja jamče da ćete znati izvući maksimum iz raspoloživih budžeta za oglašavanje. Tek certifikacijom dajete formalni temelj vašem neprestanom usavršavanju u području internetskog oglašavanja. Ovo je prvi, pravi i nezaobilazni korak ako želite promovirati sebe kao vrhunskog profesionalca na tržištu internetskog marketingImunologija (lat. immunitas - otpornost, neprimljivost; grč. lógos - nauka) je grana biomedicinskih znanosti koja proučava cjelokupnu otpornost organizma na djelovanje stranih tvari, tj. antigena. U pratičnom smislu, imunologija proučava sve oblike obrane domaćina od zaraze i štetne posljedice imunosnog odgovora za domaćina. Suvremena imunologija obuhvaća velik broj disciplina koje istražuju ustrojstva i fiziološke funkcije imunološkog sustava, imunost na zarazu, stanja imunološke preosjetljivosti (alergije), imunološke nedostatnosti (imunodeficijencije), imunološke reakcije na presađivanje i tumor, autoimunosne bolesti, kao i brojne imunodijagnostičke postupke.",
                    Datum = new DateTime(2018, 2, 16, 9, 30, 0),
                    Popunjen = false
                },

                 new Models.Seminar
                 {
                     Id = 5,
                     Naziv = "Fitopatologija",
                     Opis = "Fitopatologija (lat. 'phyton' = biljka; 'pathos' = bolest, patnja; 'logos' = nauka) je znanost o biljnim bolestima. Bavi se proučavanjem patogenih mikroorganizama i abiotičkih faktora koji izazivaju biljne bolesti, mehanizmom njihovog nastanka, interakcijom uzroka i oboljele biljke i pronalaženjem mjera za njihovo suzbijanje.\nBolest je patološki proces koji nastaje kao rezultat djelovanja parazita, biljke domaćina i utjecaja faktora sredine. Bolešću se označavaju promjene u morfološkim i fiziološkim osobinama biljaka, koje ugrožavaju njihov razvitak, smanjuju prinos i pogoršavaju kvalitetu i na kraju izazivajući njihovo propadanje. Bolest je serija vidljivih i nevidljivih odgovora biljnih stanica i tkiva na patogene mikroorganizme ili faktore vanjske sredine koji rezultiraju promjenama oblika, funkcije ili integriteta biljaka. Patogeni djeluju na biljku enzimima, toksinima ili regulatorima rasta, kako bi si osigurali hranu. Patološki proces obuhvaća i reakciju biljke na akciju patogena i to u velikoj mjeri zavisi od čimbenika okoline.",
                     Datum = new DateTime(2017, 12, 4, 11, 45, 0),
                     Popunjen = true
                 }
                );
        }
    }
}