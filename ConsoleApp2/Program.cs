using static System.Console;



var rng = new Random(123);


public interface ISelection {
    int[] Select(IReadOnlyList<Chromosome> population, int count, Random rng);
}

public interface ICrossover {
    (Chromosome Child1, Chromosome Child2) Cross(Chromosome parent1, Chromosome parent2, Random rng);
}

public interface IMutation {
    Chromosome Mutate(Chromosome source, Random rng);
}


public sealed class Chromosome {
    private readonly bool[] _genes;

    public IReadOnlyList<bool> Genes => _genes;
    public double Fitness { get; }
    public int Length => _genes.Length;

    public Chromosome(bool[] genes, double fitness) {
        if (genes == null) throw new ArgumentNullException(nameof(genes));
        if (genes.Length < 1) throw new ArgumentException("Длина хромосомы не может быть меньше 1.", nameof(genes));

        _genes = new bool[genes.Length];
        Array.Copy(genes, _genes, genes.Length);
        Fitness = fitness;
    }

    public Chromosome Clone() {
        return new Chromosome(_genes, Fitness);
    }

    public bool this[int index] => _genes[index];
}

public sealed class BitFlipMutation : IMutation {
    private readonly double _mutationProbability;

    public BitFlipMutation(double mutationProbability) {
        if (mutationProbability < 0.0 || mutationProbability > 1.0) throw new ArgumentOutOfRangeException(nameof(mutationProbability));
        _mutationProbability = mutationProbability;
    }

    public Chromosome Mutate(Chromosome source, Random rng) {
        var clone = source.Clone();

        for (int i =0; i < clone.Length; i++) {
            if (rng.NextDouble() < _mutationProbability) {
                var newGenes = (bool[])clone.Genes; newGenes[i] = !newGenes[i];
            }
        }
        return clone;
    }
}

public sealed class SinglePointCrossover : ICrossover {
    public readonly double _crossoverProbability;

    public SinglePointCrossover(double crossoverProbability) {
        if (crossoverProbability < 0.0 || crossoverProbability > 1.0) throw new ArgumentOutOfRangeException(nameof(crossoverProbability));
        _crossoverProbability = crossoverProbability;
    }

    public Chromosome Cross() {
        
    }
}
