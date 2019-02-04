package ru.tinkoff.repositories;

import ru.tinkoff.models.*;

import java.util.ArrayList;
import java.util.Collection;

public final class QuestionRepository {
    public Collection<Question> getByJobCode(String jobCode) {
        var collection = new ArrayList<Question>();
        collection.add(new TextQuestion("Как у тебя дела?", true));
        collection.add(new RichTextQuestion("Расскажи о себе", false));
        collection.add(new FileQuestion("Прикрепите своё резюме тут", true));

        var options = new ArrayList<Option>();
        options.add(new Option("Option #1"));
        options.add(new Option("Option #2"));
        options.add(new Option("Option #3"));
        collection.add(new ChoiceQuestion("Выберите один из вариантов", true, options));

        var multipleOptions = new ArrayList<Option>();
        multipleOptions.add(new Option("MultOption #1"));
        multipleOptions.add(new Option("MultOption #2"));
        multipleOptions.add(new Option("MultOption #3"));
        collection.add(new MultipleChoiceQuestion("Выберите много вариантов", false, multipleOptions));

        return collection;
    }
}
