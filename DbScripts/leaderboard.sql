CREATE TABLE public.leaderboard
(
    id SERIAL PRIMARY KEY,
    name varchar(12) NOT NULL,
    score integer NOT NULL
)