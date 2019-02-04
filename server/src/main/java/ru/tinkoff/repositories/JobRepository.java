package ru.tinkoff.repositories;

import ru.tinkoff.models.Job;

import java.util.ArrayList;
import java.util.Collection;

public final class JobRepository {
    public Job getJobByCode(String code) {
        return getAll().stream()
                .filter(job -> job.getCode().equals(code))
                .findFirst()
                .orElse(null);
    }

    public Collection<Job> getAll() {
        Collection<Job> jobs = new ArrayList<>();
        jobs.add(new Job("DEV", "Software Developer"));
        jobs.add(new Job("QA", "Quality Assurance"));
        jobs.add(new Job("QA_AUT", "QA Automation"));

        return jobs;
    }
}
