package ru.tinkoff.services;

import ru.tinkoff.exceptions.JobNotFoundException;
import ru.tinkoff.models.Job;
import ru.tinkoff.repositories.JobRepository;

public final class JobService {
    private final JobRepository jobRepository;

    public JobService() {
        this.jobRepository = new JobRepository();
    }

    public Job getJob(String code) {
        Job job = this.jobRepository.getJobByCode(code);
        if (job == null) {
            throw new JobNotFoundException(code);
        }

        return job;
    }
}
